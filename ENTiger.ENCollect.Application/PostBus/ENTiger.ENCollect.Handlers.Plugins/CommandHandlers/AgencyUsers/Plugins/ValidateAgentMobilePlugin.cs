using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class ValidateAgentMobilePlugin : FlexiPluginBase, IFlexiPlugin<ValidateAgentMobilePostBusDataPacket>
    {
        public override string Id { get; set; } = "3a1486c04cdc947cff51c62752060e5b";
        public override string FriendlyName { get; set; } = "ValidateAgentMobilePlugin";

        protected string EventCondition = "";

        protected readonly ILogger<ValidateAgentMobilePlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected AgencyUser? _model;
        protected FlexAppContextBridge? _flexAppContext;

        public ValidateAgentMobilePlugin(ILogger<ValidateAgentMobilePlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(ValidateAgentMobilePostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            _model = _flexHost.GetDomainModel<AgencyUser>().ValidateAgentMobile(packet.Cmd);

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