using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    public partial class SendEMailForDepositSlipCreated : ISendEMailForDepositSlipCreated
    {
        protected readonly ILogger<SendEMailForDepositSlipCreated> _logger;
        protected string EventCondition = "";
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;

        public SendEMailForDepositSlipCreated(ILogger<SendEMailForDepositSlipCreated> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(DepositSlipCreatedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; 

            await this.Fire<SendEMailForDepositSlipCreated>(EventCondition, serviceBusContext);
        }
    }
}