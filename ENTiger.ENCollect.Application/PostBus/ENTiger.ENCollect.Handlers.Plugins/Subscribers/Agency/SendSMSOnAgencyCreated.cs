using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class SendSMSOnAgencyCreated : ISendSMSOnAgencyCreated
    {
        protected readonly ILogger<SendSMSOnAgencyCreated> _logger;
        protected string EventCondition = "";
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;

        public SendSMSOnAgencyCreated(ILogger<SendSMSOnAgencyCreated> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(AgencyCreatedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext;
            await this.Fire<SendSMSOnAgencyCreated>(EventCondition, serviceBusContext);
        }
    }
}