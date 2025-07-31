using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class GetKeyPlugin : FlexiPluginBase, IFlexiPlugin<GetKeyPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a12db0df3b793f1fe4ac64db0c8ddfa";
        public override string FriendlyName { get; set; } = "GetKeyPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<GetKeyPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected UserLoginKeys? _model;
        protected FlexAppContextBridge? _flexAppContext;

        public GetKeyPlugin(ILogger<GetKeyPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(GetKeyPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            _model = _flexHost.GetDomainModel<UserLoginKeys>().GetKey(packet.Cmd);

            _repoFactory.GetRepo().InsertOrUpdate(_model);
            int records = await _repoFactory.GetRepo().SaveAsync();
            if (records > 0)
            {
                _logger.LogDebug("{Entity} with {EntityId} inserted into Database: ", typeof(UserLoginKeys).Name, _model.Id);
                packet.ReferenceId = _model.Id;
                packet.Key = _model.Key;
            }
            else
            {
                _logger.LogWarning("No records inserted for {Entity} with {EntityId}", typeof(UserLoginKeys).Name, _model.Id);
            }

            // TODO: Specify your condition to raise event here...
            //TODO: Set the value of EventCondition according to your business logic

            //Example:
            //EventCondition = CONDITION_ONSUCCESS;

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}