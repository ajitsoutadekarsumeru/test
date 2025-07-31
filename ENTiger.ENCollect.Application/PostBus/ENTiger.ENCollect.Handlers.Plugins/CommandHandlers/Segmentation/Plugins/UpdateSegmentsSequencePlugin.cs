using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.SegmentationModule
{
    public partial class UpdateSegmentsSequencePlugin : FlexiPluginBase, IFlexiPlugin<UpdateSegmentsSequencePostBusDataPacket>
    {
        public override string Id { get; set; } = "3a1458c61d6181c5c57e5b019a273a86";
        public override string FriendlyName { get; set; } = "UpdateSegmentsSequencePlugin";

        protected string EventCondition = "";

        protected readonly ILogger<UpdateSegmentsSequencePlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected Segmentation? _model;
        protected FlexAppContextBridge? _flexAppContext;

        public UpdateSegmentsSequencePlugin(ILogger<UpdateSegmentsSequencePlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(UpdateSegmentsSequencePostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            foreach (var item in packet.Cmd.Dto.input)
            {
                _model = await _repoFactory.GetRepo().FindAll<Segmentation>().Where(m => m.Id == item.Id).FirstOrDefaultAsync();

                if (_model != null)
                {
                    _model.UpdateSegmentsSequence(packet.Cmd, item.SequenceNumber);
                    _repoFactory.GetRepo().InsertOrUpdate(_model);

                    int records = await _repoFactory.GetRepo().SaveAsync();
                    if (records > 0)
                    {
                        _logger.LogDebug("{Entity} with {EntityId} updated into Database: ", typeof(Segmentation).Name, _model.Id);
                    }
                    else
                    {
                        _logger.LogWarning("No records updated for {Entity} with {EntityId}", typeof(Segmentation).Name, _model.Id);
                    }

                    // TODO: Specify your condition to raise event here...
                    //TODO: Set the value of OnRaiseEventCondition according to your business logic

                    //Example:
                    //EventCondition = CONDITION_ONSUCCESS;
                }
                else
                {
                    _logger.LogWarning("{Entity} with {EntityId} not found in Database: ", typeof(Segmentation).Name, item.Id);

                    //you may raise an event here to notify about the error
                    //Example:
                    //EventCondition = CONDITION_ONFAILED;
                }
            }
            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}