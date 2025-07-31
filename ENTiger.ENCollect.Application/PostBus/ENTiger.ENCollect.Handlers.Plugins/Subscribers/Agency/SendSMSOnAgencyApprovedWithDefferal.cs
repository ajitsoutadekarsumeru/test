using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class SendSMSOnAgencyApprovedWithDefferal : ISendSMSOnAgencyApprovedWithDefferal
    {
        protected readonly ILogger<SendSMSOnAgencyApprovedWithDefferal> _logger;
        protected string EventCondition = ""; 
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;

        public SendSMSOnAgencyApprovedWithDefferal(ILogger<SendSMSOnAgencyApprovedWithDefferal> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(ApproveAgencyWithDefferal @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext;
            await this.Fire<SendSMSOnAgencyApprovedWithDefferal>(EventCondition, serviceBusContext);
        }
    }
}