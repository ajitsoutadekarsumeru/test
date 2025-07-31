using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class SendSMSOnAgentApproved : ISendSMSOnAgentApproved
    {
        protected readonly ILogger<SendSMSOnAgentApproved> _logger;
        protected string EventCondition = "";
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;

        public SendSMSOnAgentApproved(ILogger<SendSMSOnAgentApproved> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(AgentApproved @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext;
            await this.Fire<SendSMSOnAgentApproved>(EventCondition, serviceBusContext);
        }
    }
}