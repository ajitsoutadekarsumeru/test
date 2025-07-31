using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class SendSMSOnAgencyDisabled : ISendSMSOnAgencyDisabled
    {
        protected readonly ILogger<SendSMSOnAgencyDisabled> _logger;
        protected string EventCondition = "";
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;

        public SendSMSOnAgencyDisabled(ILogger<SendSMSOnAgencyDisabled> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(AgencyDisabled @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext;
            await this.Fire<SendSMSOnAgencyDisabled>(EventCondition, serviceBusContext);
        }
    }
}