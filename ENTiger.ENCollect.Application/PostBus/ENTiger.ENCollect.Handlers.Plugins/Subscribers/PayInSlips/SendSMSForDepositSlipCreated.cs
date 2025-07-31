using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    public partial class SendSMSForDepositSlipCreated : ISendSMSForDepositSlipCreated
    {
        protected readonly ILogger<SendSMSForDepositSlipCreated> _logger;
        protected string EventCondition = "";
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;

        public SendSMSForDepositSlipCreated(ILogger<SendSMSForDepositSlipCreated> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(DepositSlipCreatedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext;

            await this.Fire<SendSMSForDepositSlipCreated>(EventCondition, serviceBusContext);
        }
    }
}