using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    public partial class DisableTreatmentsPlugin : FlexiPluginBase, IFlexiPlugin<DisableTreatmentsPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a14633033fc9965976d06f315b676d3";
        public override string FriendlyName { get; set; } = "DisableTreatmentsPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<DisableTreatmentsPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected List<Treatment>? _model;
        protected FlexAppContextBridge? _flexAppContext;

        public DisableTreatmentsPlugin(ILogger<DisableTreatmentsPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(DisableTreatmentsPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            _model = await _repoFactory.GetRepo().FindAll<Treatment>().Where(m => packet.Cmd.Dto.TreatmentIds.Contains(m.Id)).ToListAsync();

            if (_model != null)
            {
                foreach (var obj in _model)
                {
                    obj.DisableTreatments(packet.Cmd);
                    _repoFactory.GetRepo().InsertOrUpdate(obj);
                }

                int records = await _repoFactory.GetRepo().SaveAsync();
                if (records > 0)
                {
                    _logger.LogDebug("{Entity} with {EntityId} updated into Database: ", typeof(Treatment).Name, string.Join(", ", packet.Cmd.Dto.TreatmentIds));
                }
                else
                {
                    _logger.LogWarning("No records updated for {Entity} with {EntityId}", typeof(Treatment).Name, string.Join(", ", packet.Cmd.Dto.TreatmentIds));
                }
                // TODO: Specify your condition to raise event here...
                //TODO: Set the value of OnRaiseEventCondition according to your business logic

                //Example:
                //EventCondition = CONDITION_ONSUCCESS;
            }
            else
            {
                _logger.LogWarning("{Entity} with {EntityId} not found in Database: ", typeof(Treatment).Name, string.Join(", ", packet.Cmd.Dto.TreatmentIds));

                //you may raise an event here to notify about the error
                //Example:
                //EventCondition = CONDITION_ONFAILED;
            }
            //await this.Fire(EventCondition, packet.FlexServiceBusContext);
            await Task.CompletedTask;
        }
    }
}