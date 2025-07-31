using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class SendSMSOnAgencyApproved : ISendSMSOnAgencyApproved
    {
        protected readonly ILogger<SendSMSOnAgencyApproved> _logger;
        protected string EventCondition = "";
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;

        public SendSMSOnAgencyApproved(ILogger<SendSMSOnAgencyApproved> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(AgencyApproved @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; 
            await this.Fire<SendSMSOnAgencyApproved>(EventCondition, serviceBusContext);
        }
    }
}