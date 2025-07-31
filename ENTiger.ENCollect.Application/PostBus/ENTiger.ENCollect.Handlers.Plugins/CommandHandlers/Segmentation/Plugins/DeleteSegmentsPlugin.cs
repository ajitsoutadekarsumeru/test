using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.SegmentationModule
{
    public partial class DeleteSegmentsPlugin : FlexiPluginBase, IFlexiPlugin<DeleteSegmentsPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a1458c99db9c80384e8d0666a01ba0e";
        public override string FriendlyName { get; set; } = "DeleteSegmentsPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<DeleteSegmentsPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected Segmentation? _model;
        protected FlexAppContextBridge? _flexAppContext;

        public DeleteSegmentsPlugin(ILogger<DeleteSegmentsPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(DeleteSegmentsPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            foreach (var segmentId in packet.Cmd.Dto.SegmentIds)
            {
                _model = await _repoFactory.GetRepo().FindAll<Segmentation>().Where(a => a.Id == segmentId).FirstOrDefaultAsync();

                if (_model != null)
                {
                    _model.DeleteSegments(segmentId);
                    _repoFactory.GetRepo().InsertOrUpdate(_model);
                    int records = await _repoFactory.GetRepo().SaveAsync();
                    if (records > 0)
                    {
                        _logger.LogDebug("{Entity} with {EntityId} deleted from Database: ", typeof(Segmentation).Name, _model.Id);
                    }
                    else
                    {
                        _logger.LogWarning("No records deleted for {Entity} with {EntityId}", typeof(Segmentation).Name, _model.Id);
                    }
                }
                else
                {
                    _logger.LogWarning("Segmentation not found in Database: " + packet.Cmd.Dto.Id);

                    //you may raise an event here to notify about the error
                    //Example:
                    //EventCondition = CONDITION_ONFAILED;
                }

                //TODO: Specify your condition to raise event here...
                //TODO: Set the value of OnRaiseEventCondition according to your business logic

                //EventCondition = CONDITION_ONSUCCESS;
            }

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}