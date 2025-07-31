using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    public partial class DeleteTreatmentsPlugin : FlexiPluginBase, IFlexiPlugin<DeleteTreatmentsPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a14632d9e63203059a83d6f8c2230db";
        public override string FriendlyName { get; set; } = "DeleteTreatmentsPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<DeleteTreatmentsPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected List<Treatment>? _model;
        protected FlexAppContextBridge? _flexAppContext;

        public DeleteTreatmentsPlugin(ILogger<DeleteTreatmentsPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(DeleteTreatmentsPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            _model = await _repoFactory.GetRepo().FindAll<Treatment>().Where(m => packet.Cmd.Dto.TreatmentId.Contains(m.Id))
                            .FlexInclude(a => a.segmentMapping)
                            .FlexInclude(a => a.subTreatment)
                            .FlexInclude("subTreatment.POSCriteria")
                            .FlexInclude("subTreatment.AccountCriteria")
                            .FlexInclude("subTreatment.RoundRobinCriteria")
                            .FlexInclude("subTreatment.TreatmentByRule")
                            .FlexInclude("subTreatment.TreatmentOnUpdateTrail")
                            .FlexInclude("subTreatment.TreatmentOnCommunication")
                            .FlexInclude("subTreatment.PerformanceBand")
                            .FlexInclude("subTreatment.Designation").ToListAsync();

            if (_model != null)
            {
                foreach (var obj in _model)
                {
                    obj.DeleteTreatments(packet.Cmd);
                    _repoFactory.GetRepo().InsertOrUpdate(obj);
                }

                int records = await _repoFactory.GetRepo().SaveAsync();
                if (records > 0)
                {
                    _logger.LogDebug("{Entity} with {EntityId} deleted from Database: ", typeof(Treatment).Name, string.Join(", ", packet.Cmd.Dto.TreatmentId));
                }
                else
                {
                    _logger.LogWarning("No records deleted for {Entity} with {EntityId}", typeof(Treatment).Name, string.Join(", ", packet.Cmd.Dto.TreatmentId));
                }

                //TODO: Specify your condition to raise event here...
                //TODO: Set the value of OnRaiseEventCondition according to your business logic

                //EventCondition = CONDITION_ONSUCCESS;
            }
            else
            {
                _logger.LogWarning("Treatment not found in Database: " + string.Join(", ", packet.Cmd.Dto.TreatmentId));
                //you may raise an event here to notify about the error
                //Example:
                //EventCondition = CONDITION_ONFAILED;
            }
            //await this.Fire(EventCondition, packet.FlexServiceBusContext);
            await Task.CompletedTask;
        }
    }
}