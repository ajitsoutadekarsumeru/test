using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class SendEmailOnAgentRejected : ISendEmailOnAgentRejected
    {
        protected readonly ILogger<SendEmailOnAgentRejected> _logger;
        protected string EventCondition = "";
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;

        public SendEmailOnAgentRejected(ILogger<SendEmailOnAgentRejected> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(AgentRejected @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext;
            await this.Fire<SendEmailOnAgentRejected>(EventCondition, serviceBusContext);
        }
    }
}