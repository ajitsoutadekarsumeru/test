using Elastic.Transport;
using ENTiger.ENCollect.TreatmentModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Sumeru.Flex;
using System.Data;

namespace ENTiger.ENCollect
{
    public class ExecuteTreatmentGetSegmentIdFromElasticSearchFFPlugin : FlexiPluginBase, IFlexiPlugin<ExecuteTreatmentPostBusDataPacket>
    {
        public override string Id { get; set; } = "ExecuteTreatmentGetSegmentIdFromElasticSearchFFPlugin";
        public override string FriendlyName { get; set; } = "ExecuteTreatmentGetSegmentIdFromElasticSearchFFPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<ExecuteTreatmentPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IFlexServiceBusBridge _bus;
        private readonly IELKUtility _elasticUtility;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="logger"></param>
        public ExecuteTreatmentGetSegmentIdFromElasticSearchFFPlugin(ILogger<ExecuteTreatmentPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory, IFlexServiceBusBridge bus, IELKUtility elasticUtility)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _bus = bus;
            _elasticUtility = elasticUtility;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="packet"></param>
        public virtual async Task Execute(ExecuteTreatmentPostBusDataPacket packet)
        {
            //Write your code here:
            _repoFactory.Init(packet.Cmd.Dto);
            _flexAppContext = packet.Cmd.Dto.GetAppContext();

            _logger.LogInformation("TreatmentGetSegmentIdFFPlugin : Start");
            try
            {
                List<string> segments = new List<string>();
                List<ElasticSearchSimulateLoanAccountDto> elasticloanaccountslist = new List<ElasticSearchSimulateLoanAccountDto>();

                segments = packet.segments;

                var fetchindexname = await _repoFactory.GetRepo().FindAll<FeatureMaster>()
                                                .Where(a => a.Parameter == "SegmentationIndexName").FirstOrDefaultAsync();
                string loanaccountsIndex = fetchindexname?.Value;//"mt_uat_segmentnew";
                _logger.LogInformation("TreatmentGetSegmentIdFFPlugin : LoanAccountIndexName - " + loanaccountsIndex);

                string TreatmentHistoryId = SequentialGuid.NewGuidString();
                packet.TreatmentHistoryId = TreatmentHistoryId;
                _logger.LogInformation("TreatmentGetSegmentIdFFPlugin : TreatmentHistoryId - " + TreatmentHistoryId);

                string segmentationid = segments.FirstOrDefault();
                _logger.LogInformation("TreatmentGetSegmentIdFFPlugin : SegmentationId - " + segmentationid);

                string DSLQueryForAllDocs1 = @"
                {
                  ""track_total_hits"": true,
                  ""from"":0,
                  ""size"": 0,
                  ""query"": {
                  ""terms"": {
                        ""segmentationid"":[
                ";

                DSLQueryForAllDocs1 += $@"
                ""{segmentationid}""
                 ]
                }}
                }}
                }}
                ";
                _logger.LogInformation("TreatmentGetSegmentIdFFPlugin : DSLQuery - " + DSLQueryForAllDocs1);

                var client = _elasticUtility.GetElasticConnection();

                string elasticsearchapipath = loanaccountsIndex + "/_search";

                var elkresp1 = await client.Transport.RequestAsync<StringResponse>(Elastic.Transport.HttpMethod.POST, elasticsearchapipath, PostData.String(DSLQueryForAllDocs1));

                string response1 = elkresp1.Body;
                dynamic RootObj1 = JObject.Parse(response1);
                int recordCount = RootObj1.hits.total.value;
                _logger.LogInformation("TreatmentGetSegmentIdFFPlugin : Segmentation Records count - " + recordCount);

                int fragmentedrecordCount = 0;
                if (recordCount > 0)
                {
                    var pages = (recordCount / 5000) + 1;
                    _logger.LogInformation("TreatmentGetSegmentIdFFPlugin : Pages count - " + pages);

                    for (long page = 0; page < pages; page++)
                    {
                        int fromRecord = (int)(page * 5000);
                        _logger.LogInformation("fromRecord count " + fromRecord);
                        string DSLQueryForAllDocs = @"
                        {
                          ""track_total_hits"": true,
                          ""_source"": [""AGREEMENTID"",""id"",""bom_pos"",""segmentationid"",""PAYMENTSTATUS""],
                          ""from"":";

                        DSLQueryForAllDocs += $@"
                        ""{fromRecord}"",
                        ""size"": 5000,
                          ""query"": {{
                          ""terms"": {{
                                ""segmentationid"":[
                        ";

                        DSLQueryForAllDocs += $@"
                        ""{segmentationid}""
                         ]
                        }}
                        }}
                        }}

                        ";

                        _logger.LogInformation("TreatmentGetSegmentIdFFPlugin : DSLQuery - " + DSLQueryForAllDocs);
                        var elkresp = await client.Transport.RequestAsync<StringResponse>(Elastic.Transport.HttpMethod.POST, elasticsearchapipath, PostData.String(DSLQueryForAllDocs));

                        if (elkresp != null && elkresp.Body != null)
                        {
                            string response = elkresp.Body;
                            dynamic RootObj = JObject.Parse(response);

                            if (RootObj != null && RootObj.hits != null && RootObj.hits.hits != null)
                            {
                                foreach (var res in RootObj.hits.hits)
                                {
                                    ElasticSearchSimulateLoanAccountDto loanaccount = new ElasticSearchSimulateLoanAccountDto();
                                    var obj = res._source;
                                    if (obj != null)
                                    {
                                        loanaccount.id = obj.id != null ? obj.id : "";
                                        loanaccount.segmentationid = obj.segmentationid != null ? obj.segmentationid : "";
                                        loanaccount.agreementid = obj.AGREEMENTID != null ? obj.AGREEMENTID : "";
                                        loanaccount.paymentstatus = obj.PAYMENTSTATUS != null ? obj.PAYMENTSTATUS : "";
                                        elasticloanaccountslist.Add(loanaccount);
                                    }
                                }
                                _logger.LogInformation("TreatmentGetSegmentIdFFPlugin : Elastic LoanAccountsList Count - " + elasticloanaccountslist.Count());
                                string paymentstatus = packet.outputModel != null ? packet.outputModel.PaymentStatusToStop : "";

                                _logger.LogInformation("TreatmentGetSegmentIdFFPlugin : PaymentStatus - " + paymentstatus);
                                int FinalCountOfAccounts = 0;
                                if (!string.IsNullOrEmpty(paymentstatus) && !string.Equals(paymentstatus, "notapplicable", StringComparison.OrdinalIgnoreCase))
                                {
                                    List<string> paymentstatuslist = paymentstatus.Split(',').ToList();

                                    var exceptlist = elasticloanaccountslist.Where(a => paymentstatuslist.Contains(a.paymentstatus)).ToList();
                                    _logger.LogInformation("TreatmentGetSegmentIdFFPlugin : ExceptList Count - " + exceptlist.Count());

                                    string[] exceptlistarrays = exceptlist.Select(a => a.id).ToArray();
                                    string[] fulllistarrays = elasticloanaccountslist.Select(a => a.id).ToArray();
                                    _logger.LogInformation("TreatmentGetSegmentIdFFPlugin : Fulllistarrays Count -" + fulllistarrays.Count());

                                    IEnumerable<string> finaltlist = fulllistarrays.Except(exceptlistarrays);
                                    _logger.LogInformation("TreatmentGetSegmentIdFFPlugin : Finaltlist Count - " + finaltlist.Count());

                                    var filterpaymentstatus = elasticloanaccountslist.Where(a => finaltlist.Contains(a.id)).ToList();

                                    elasticloanaccountslist = filterpaymentstatus;
                                    FinalCountOfAccounts = filterpaymentstatus.Count();
                                }
                                else
                                {
                                    _logger.LogInformation("Fragmentedrecord count " + fragmentedrecordCount);
                                    FinalCountOfAccounts = elasticloanaccountslist.Count();
                                }
                                _logger.LogInformation("TreatmentGetSegmentIdFFPlugin : FinalCountOfAccounts - " + FinalCountOfAccounts);
                            }
                        }
                    }

                    ExecuteFragmentedTreatmentDto executeFragmentedTreatmentDto = new ExecuteFragmentedTreatmentDto();
                    executeFragmentedTreatmentDto.TreatmentId = packet.Cmd.Dto.TreatmentId;
                    executeFragmentedTreatmentDto.TreatmentHistoryId = packet.TreatmentHistoryId;
                    executeFragmentedTreatmentDto.segments = packet.segments;
                    executeFragmentedTreatmentDto.loanAccounts = elasticloanaccountslist;
                    executeFragmentedTreatmentDto.treatExecutionStartdate = packet.treatExecutionStartdate;
                    executeFragmentedTreatmentDto.treatExecutionEnddate = packet.treatExecutionEnddate;
                    executeFragmentedTreatmentDto.TenantId = _flexAppContext.TenantId;
                    executeFragmentedTreatmentDto.PartyId = _flexAppContext.UserId;
                    executeFragmentedTreatmentDto.executeTreatmentDto = packet.Cmd.Dto;
                    executeFragmentedTreatmentDto.totalCountOfAccounts = recordCount;
                    executeFragmentedTreatmentDto.SetAppContext(_flexAppContext);
                    ExecuteFragmentedTreatmentCommand cmd = new ExecuteFragmentedTreatmentCommand
                    {
                        Dto = executeFragmentedTreatmentDto
                    };

                    await _bus.Send(cmd);
                }
            }
            catch (Exception ex)
            {
                string errorMsg = ex + ex?.Message + " > " + ex?.StackTrace + " > " + ex?.InnerException + ex?.InnerException?.Message + ex?.InnerException?.StackTrace;
                _logger.LogError("Exception in TreatmentGetSegmentIdFFPlugin : " + errorMsg);
            }
            _logger.LogInformation("TreatmentGetSegmentIdFFPlugin : End");

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}