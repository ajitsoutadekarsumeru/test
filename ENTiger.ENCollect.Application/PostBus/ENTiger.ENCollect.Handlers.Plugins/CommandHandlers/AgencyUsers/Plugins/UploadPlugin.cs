using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class UploadPlugin : FlexiPluginBase, IFlexiPlugin<UploadPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a136152398bbaa5bc0e8853b8e56da7";
        public override string FriendlyName { get; set; } = "UploadPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<UploadPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected AgencyUserIdentificationDoc? _model;
        protected FlexAppContextBridge? _flexAppContext;

        public UploadPlugin(ILogger<UploadPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(UploadPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            _model = _flexHost.GetDomainModel<AgencyUserIdentificationDoc>().Upload(packet.Cmd);

            _repoFactory.GetRepo().InsertOrUpdate(_model);
            int records = await _repoFactory.GetRepo().SaveAsync();
            if (records > 0)
            {
                _logger.LogDebug("{Entity} with {EntityId} inserted into Database: ", typeof(AgencyUserIdentificationDoc).Name, _model.Id);
            }
            else
            {
                _logger.LogWarning("No records inserted for {Entity} with {EntityId}", typeof(AgencyUserIdentificationDoc).Name, _model.Id);
            }
            packet.Id = _model.Id;
            // TODO: Specify your condition to raise event here...
            //TODO: Set the value of EventCondition according to your business logic

            //Example:
            //EventCondition = CONDITION_ONSUCCESS;

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}