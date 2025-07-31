using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class SendSMSOnAgencyRejected : ISendSMSOnAgencyRejected
    {
        protected readonly ILogger<SendSMSOnAgencyRejected> _logger;
        protected string EventCondition = "";
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;

        public SendSMSOnAgencyRejected(ILogger<SendSMSOnAgencyRejected> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(AgencyRejected @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext;
            await this.Fire<SendSMSOnAgencyRejected>(EventCondition, serviceBusContext);
        }
    }
}