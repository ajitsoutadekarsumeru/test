using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class LogoutPlugin : FlexiPluginBase, IFlexiPlugin<LogoutPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a1337c8a6a73e1bc335a8d81843b9c6";
        public override string FriendlyName { get; set; } = "LogoutPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<LogoutPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected ApplicationUser? _model;
        protected FlexAppContextBridge? _flexAppContext;

        public LogoutPlugin(ILogger<LogoutPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(LogoutPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            _model = _flexHost.GetDomainModel<ApplicationUser>().Logout(packet.Cmd);

            _repoFactory.GetRepo().InsertOrUpdate(_model);
            int records = await _repoFactory.GetRepo().SaveAsync();
            if (records > 0)
            {
                _logger.LogDebug("{Entity} with {EntityId} inserted into Database: ", typeof(AgencyUser).Name, _model.Id);
            }
            else
            {
                _logger.LogWarning("No records inserted for {Entity} with {EntityId}", typeof(AgencyUser).Name, _model.Id);
            }

            // TODO: Specify your condition to raise event here...
            //TODO: Set the value of EventCondition according to your business logic

            //Example:
            //EventCondition = CONDITION_ONSUCCESS;

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}