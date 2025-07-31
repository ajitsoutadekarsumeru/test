using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class ValidateAgentEmailPlugin : FlexiPluginBase, IFlexiPlugin<ValidateAgentEmailPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a1486bf5fa9c5afd360f683b83205c6";
        public override string FriendlyName { get; set; } = "ValidateAgentEmailPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<ValidateAgentEmailPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected AgencyUser? _model;
        protected FlexAppContextBridge? _flexAppContext;

        public ValidateAgentEmailPlugin(ILogger<ValidateAgentEmailPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(ValidateAgentEmailPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            _model = _flexHost.GetDomainModel<AgencyUser>().ValidateAgentEmail(packet.Cmd);

            _repoFactory.GetRepo().InsertOrUpdate(_model);
            int records = await _repoFactory.GetRepo().SaveAsync();
            if (records > 0)
            {
                _logger.LogInformation("{Entity} with {EntityId} inserted into Database ", typeof(AgencyUser).Name, _model.Id);
            }
            else
            {
                _logger.LogWarning("No records inserted for {Entity} with {EntityId} ", typeof(AgencyUser).Name, _model.Id);
            }

            // TODO: Specify your condition to raise event here...
            //TODO: Set the value of EventCondition according to your business logic

            //Example:
            //EventCondition = CONDITION_ONSUCCESS;

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}