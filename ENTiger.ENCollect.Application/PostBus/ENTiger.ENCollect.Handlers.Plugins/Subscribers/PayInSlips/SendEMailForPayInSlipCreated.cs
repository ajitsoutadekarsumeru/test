using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    public partial class SendEMailForPayInSlipCreated : ISendEMailForPayInSlipCreated
    {
        protected readonly ILogger<SendEMailForPayInSlipCreated> _logger;
        protected string EventCondition = "";
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;

        public SendEMailForPayInSlipCreated(ILogger<SendEMailForPayInSlipCreated> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(PayInSlipCreatedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; 
            await this.Fire<SendEMailForPayInSlipCreated>(EventCondition, serviceBusContext);
        }
    }
}