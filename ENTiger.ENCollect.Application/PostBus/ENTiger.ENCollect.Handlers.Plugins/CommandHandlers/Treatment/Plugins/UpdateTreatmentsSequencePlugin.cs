using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    public partial class UpdateTreatmentsSequencePlugin : FlexiPluginBase, IFlexiPlugin<UpdateTreatmentsSequencePostBusDataPacket>
    {
        public override string Id { get; set; } = "3a14632b022f10ca9892d17cf43df45e";
        public override string FriendlyName { get; set; } = "UpdateTreatmentsSequencePlugin";

        protected string EventCondition = "";

        protected readonly ILogger<UpdateTreatmentsSequencePlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected Treatment? _model;
        protected FlexAppContextBridge? _flexAppContext;

        public UpdateTreatmentsSequencePlugin(ILogger<UpdateTreatmentsSequencePlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(UpdateTreatmentsSequencePostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            foreach (var item in packet.Cmd.Dto.input)
            {
                _model = await _repoFactory.GetRepo().FindAll<Treatment>().Where(m => m.Id == item.Id).FirstOrDefaultAsync();

                if (_model != null)
                {
                    _model.UpdateTreatmentsSequence(packet.Cmd, item.Sequence);
                    _repoFactory.GetRepo().InsertOrUpdate(_model);

                    int records = await _repoFactory.GetRepo().SaveAsync();
                    if (records > 0)
                    {
                        _logger.LogDebug("{Entity} with {EntityId} updated into Database: ", typeof(Treatment).Name, _model.Id);
                    }
                    else
                    {
                        _logger.LogWarning("No records updated for {Entity} with {EntityId}", typeof(Treatment).Name, _model.Id);
                    }
                }
                else
                {
                    _logger.LogWarning("{Entity} with {EntityId} not found in Database: ", typeof(Treatment).Name);
                }
            }

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}