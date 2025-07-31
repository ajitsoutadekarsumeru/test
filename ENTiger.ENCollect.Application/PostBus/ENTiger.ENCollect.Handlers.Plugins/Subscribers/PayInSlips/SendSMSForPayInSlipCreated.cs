using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    public partial class SendSMSForPayInSlipCreated : ISendSMSForPayInSlipCreated
    {
        protected readonly ILogger<SendSMSForPayInSlipCreated> _logger;
        protected string EventCondition = "";
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;

        public SendSMSForPayInSlipCreated(ILogger<SendSMSForPayInSlipCreated> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(PayInSlipCreatedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext;
            await this.Fire<SendSMSForPayInSlipCreated>(EventCondition, serviceBusContext);
        }
    }
}