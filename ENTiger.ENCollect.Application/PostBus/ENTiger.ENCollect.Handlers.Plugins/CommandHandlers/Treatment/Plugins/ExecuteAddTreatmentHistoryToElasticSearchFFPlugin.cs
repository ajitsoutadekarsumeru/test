using ENTiger.ENCollect.TreatmentModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class ExecuteAddTreatmentHistoryToElasticSearchFFPlugin : FlexiPluginBase, IFlexiPlugin<ExecuteFragmentedTreatmentDataPacket>
    {
        public override string Id { get; set; } = "ExecuteTreatmentAddTreatmentHistoryFFPlugin";
        public override string FriendlyName { get; set; } = "ExecuteTreatmentAddTreatmentHistoryFFPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<ExecuteAddTreatmentHistoryToElasticSearchFFPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected Treatment? _model;
        protected FlexAppContextBridge? _flexAppContext;

        public ExecuteAddTreatmentHistoryToElasticSearchFFPlugin(ILogger<ExecuteAddTreatmentHistoryToElasticSearchFFPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(ExecuteFragmentedTreatmentDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto.executeTreatmentDto);

            _logger.LogInformation("Reached AddTreatmentHistoryToElasticSearchFFPlugin ");
            List<ElasticSearchSimulateLoanAccountDto> accounts = packet.Cmd.Dto.loanAccounts;
            List<TreatmentHistoryDetails> loanAccounts = new List<TreatmentHistoryDetails>();

            if (packet.Cmd.Dto.totalCountOfAccounts > 0)
            {
                _logger.LogInformation("AddTreatmentHistoryToElasticSearchFFPlugin " + packet.Cmd.Dto.loanAccounts.Count());
                var ids = packet.Cmd.Dto.loanAccounts.Select(a => a.agreementid).ToList();
                _logger.LogInformation("AddTreatmentHistoryToElasticSearchFFPlugin agreementids ids " + ids.Count());
                var updatedLoanaccounts = await _repoFactory.GetRepo().FindAll<LoanAccount>().Where(a => ids.Contains(a.AGREEMENTID)).ToListAsync();
                foreach (var x in updatedLoanaccounts)
                {
                    TreatmentHistoryDetails elastictreatmenthistory = new TreatmentHistoryDetails();

                    var elasticAccount = accounts.Where(a => a.id == x.Id).FirstOrDefault();

                    var newaccount = x;//updatedLoanaccounts.Where(b => b.Id == a.id).FirstOrDefault();
                    elastictreatmenthistory.allocationownerid = newaccount != null ? newaccount.AllocationOwnerId : "";
                    elastictreatmenthistory.telecallerid = newaccount != null ? newaccount.TeleCallerId : "";
                    elastictreatmenthistory.telecallingagencyid = newaccount != null ? newaccount.TeleCallingAgencyId : "";
                    elastictreatmenthistory.collectorid = newaccount != null ? newaccount.CollectorId : "";
                    elastictreatmenthistory.agencyid = newaccount != null ? newaccount.AgencyId : "";
                    elastictreatmenthistory.treatmentid = packet.Cmd.Dto.TreatmentId;
                    elastictreatmenthistory.treatmenthistoryid = packet.Cmd.Dto.TreatmentHistoryId;
                    elastictreatmenthistory.SetId(SequentialGuid.NewGuidString());
                    elastictreatmenthistory.agreementid = newaccount.AGREEMENTID;
                    elastictreatmenthistory.bom_pos = newaccount.BOM_POS != null ? Convert.ToDouble(newaccount.BOM_POS) : 0;
                    elastictreatmenthistory.branch = newaccount.BRANCH;
                    elastictreatmenthistory.bucket = newaccount.BUCKET != null ? newaccount.BUCKET.ToString() : "";
                    elastictreatmenthistory.city = newaccount.CITY;
                    elastictreatmenthistory.current_bucket = newaccount.CURRENT_BUCKET;
                    elastictreatmenthistory.current_dpd = newaccount.CURRENT_DPD != null ? newaccount.CURRENT_DPD.ToString() : string.Empty; //newaccount.CURRENT_DPD.ToString();
                    elastictreatmenthistory.current_pos = newaccount.CURRENT_POS;
                    elastictreatmenthistory.customerid = newaccount.CUSTOMERID;
                    elastictreatmenthistory.customername = newaccount.CUSTOMERNAME;
                    elastictreatmenthistory.dispcode = newaccount.DispCode;
                    elastictreatmenthistory.latestmobileno = newaccount.LatestMobileNo;
                    elastictreatmenthistory.npa_stageid = newaccount.NPA_STAGEID;
                    elastictreatmenthistory.principal_od = newaccount.PRINCIPAL_OD != null ? newaccount.PRINCIPAL_OD.ToString() : "";
                    elastictreatmenthistory.product = newaccount.PRODUCT;
                    elastictreatmenthistory.productgroup = newaccount.ProductGroup;
                    elastictreatmenthistory.region = newaccount.Region;
                    elastictreatmenthistory.segmentationid = elasticAccount != null ? elasticAccount.segmentationid : "";
                    elastictreatmenthistory.state = newaccount.STATE;
                    elastictreatmenthistory.subproduct = newaccount.SubProduct;
                    elastictreatmenthistory.tos = newaccount.TOS;
                    elastictreatmenthistory.SetLastModifiedDate(DateTimeOffset.Now);
                    elastictreatmenthistory.SetCreatedDate(DateTimeOffset.Now);
                    elastictreatmenthistory.SetAdded();
                    loanAccounts.Add(elastictreatmenthistory);
                }

                foreach (var acc in loanAccounts)
                {
                    _repoFactory.GetRepo().InsertOrUpdate(acc);
                }

                _logger.LogInformation(" AddTreatmentHistoryToElasticSearchFFPlugin before save ");
                await _repoFactory.GetRepo().SaveAsync();

                _logger.LogInformation(" AddTreatmentHistoryToElasticSearchFFPlugin after save ");

                _logger.LogInformation("Completed AddTreatmentHistoryToElasticSearchFFPlugin ");
            }

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}