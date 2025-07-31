using ENTiger.ENCollect.TreatmentModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class ExecuteTreatmentAddTreatmentHistoryFFPlugin : FlexiPluginBase, IFlexiPlugin<ExecuteFragmentedTreatmentDataPacket>
    {
        public override string Id { get; set; } = "ExecuteTreatmentAddTreatmentHistoryFFPlugin";
        public override string FriendlyName { get; set; } = "ExecuteTreatmentAddTreatmentHistoryFFPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<ExecuteTreatmentAddTreatmentHistoryFFPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected Treatment? _model;
        protected FlexAppContextBridge? _flexAppContext;

        public ExecuteTreatmentAddTreatmentHistoryFFPlugin(ILogger<ExecuteTreatmentAddTreatmentHistoryFFPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(ExecuteFragmentedTreatmentDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line

            _repoFactory.Init(packet.Cmd.Dto.executeTreatmentDto);

            TreatmentHistory treatmentHistory = await ConstructTreatmentHistoryFromModel(packet, packet.Cmd.Dto.totalCountOfAccounts, packet.Cmd.Dto.TreatmentHistoryId);

            _repoFactory.GetRepo().InsertOrUpdate(treatmentHistory);
            await _repoFactory.GetRepo().SaveAsync();

            var outputModel = await _repoFactory.GetRepo().FindAll<Treatment>()
                                    .FlexInclude(a => a.subTreatment)
                                    .FlexInclude("subTreatment.POSCriteria")
                                    .FlexInclude("subTreatment.AccountCriteria")
                                    .FlexInclude("subTreatment.RoundRobinCriteria")
                                    .FlexInclude("subTreatment.TreatmentByRule")
                                    .FlexInclude("subTreatment.TreatmentOnUpdateTrail")
                                    .FlexInclude("subTreatment.TreatmentOnCommunication")
                                    .FlexInclude("subTreatment.PerformanceBand")
                                    .FlexInclude("subTreatment.Designation")
                                    .FlexInclude("subTreatment.DeliveryStatus")
                                    .FlexInclude(a => a.segmentMapping)
                                    .FlexInclude("segmentMapping.Segment")
                                    .Where(a => a.Id == packet.Cmd.Dto.TreatmentId).FirstOrDefaultAsync();

            packet.outputModel = outputModel;
        }

        private async Task<TreatmentHistory> ConstructTreatmentHistoryFromModel(ExecuteFragmentedTreatmentDataPacket packet, int totalCountOfAccounts, string TreatmentHistoryId)
        {
            try
            {
                TreatmentHistory treatmentHistory = new TreatmentHistory();

                treatmentHistory = await _repoFactory.GetRepo().FindAll<TreatmentHistory>().Where(a => a.Id == TreatmentHistoryId).FirstOrDefaultAsync();

                if (treatmentHistory != null)
                {
                    int treatmenthistorycount = treatmentHistory.NoOfAccounts != null ? Convert.ToInt32(treatmentHistory.NoOfAccounts) : 0;
                    int sumupcount = treatmenthistorycount + totalCountOfAccounts;

                    _logger.LogInformation("Sumup count " + sumupcount);
                    treatmentHistory.NoOfAccounts = Convert.ToString(sumupcount);
                    treatmentHistory.SetModified();
                    return treatmentHistory;
                }
                else
                {
                    TreatmentHistory treatmentHistoryNew = new TreatmentHistory();
                    //SequentialGuid.NewGuidString();
                    treatmentHistoryNew.TreatmentId = packet.Cmd.Dto.TreatmentId;
                    treatmentHistoryNew.NoOfAccounts = Convert.ToString(totalCountOfAccounts);
                    treatmentHistoryNew.SetCreatedBy(packet.Cmd.Dto.PartyId);
                    //treatmentHistoryNew.SetIsDeleted(false);
                    //treatmentHistoryNew.SetCreatedDate(DateTime.Now);
                    treatmentHistoryNew.SetLastModifiedBy(packet.Cmd.Dto.PartyId);
                    //treatmentHistoryNew.SetLastModifiedDate(DateTime.Now);
                    //treatmentHistoryNew.SetAddedOrModified();
                    treatmentHistoryNew.SetAdded();
                    treatmentHistoryNew.SetId(TreatmentHistoryId);
                    return treatmentHistoryNew;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in ConstructTreatmentHistoryFromModel " + ex);
                throw ex;
            }
        }
    }
}