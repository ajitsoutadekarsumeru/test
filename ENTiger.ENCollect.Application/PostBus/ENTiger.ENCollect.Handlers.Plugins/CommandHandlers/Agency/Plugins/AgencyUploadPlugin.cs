using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class AgencyUploadPlugin : FlexiPluginBase, IFlexiPlugin<AgencyUploadPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a13619659ed4658c8b97889c5d39022";
        public override string FriendlyName { get; set; } = "AgencyUploadPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<AgencyUploadPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected AgencyIdentificationDoc? _model;
        protected FlexAppContextBridge? _flexAppContext;

        public AgencyUploadPlugin(ILogger<AgencyUploadPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(AgencyUploadPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            _model = _flexHost.GetDomainModel<AgencyIdentificationDoc>().AgencyUpload(packet.Cmd);

            _repoFactory.GetRepo().InsertOrUpdate(_model);
            int records = await _repoFactory.GetRepo().SaveAsync();
            if (records > 0)
            {
                _logger.LogDebug("{Entity} with {EntityId} inserted into Database: ", typeof(AgencyIdentificationDoc).Name, _model.Id);
            }
            else
            {
                _logger.LogWarning("No records inserted for {Entity} with {EntityId}", typeof(AgencyIdentificationDoc).Name, _model.Id);
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