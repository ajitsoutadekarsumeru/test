using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.GeoTagModule
{
    public partial class AddGeoTagPlugin : FlexiPluginBase, IFlexiPlugin<AddGeoTagPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a13679ad266b72ba9011db50136fcd6";
        public override string FriendlyName { get; set; } = "AddGeoTagPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<AddGeoTagPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected GeoTagDetails? _model;
        protected FlexAppContextBridge? _flexAppContext;

        public AddGeoTagPlugin(ILogger<AddGeoTagPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(AddGeoTagPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            _model = _flexHost.GetDomainModel<GeoTagDetails>().AddGeoTag(packet.Cmd);

            _repoFactory.GetRepo().InsertOrUpdate(_model);
            int records = await _repoFactory.GetRepo().SaveAsync();
            if (records > 0)
            {
                _logger.LogDebug("{Entity} with {EntityId} inserted into Database: ", typeof(GeoTagDetails).Name, _model.Id);
            }
            else
            {
                _logger.LogWarning("No records inserted for {Entity} with {EntityId}", typeof(GeoTagDetails).Name, _model.Id);
            }

            // TODO: Specify your condition to raise event here...
            //TODO: Set the value of EventCondition according to your business logic

            //Example:
            //EventCondition = CONDITION_ONSUCCESS;

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}