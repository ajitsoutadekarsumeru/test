using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class SendEmailOnAgencyApprovedWithDefferal : ISendEmailOnAgencyApprovedWithDefferal
    {
        protected readonly ILogger<SendEmailOnAgencyApprovedWithDefferal> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;

        public SendEmailOnAgencyApprovedWithDefferal(ILogger<SendEmailOnAgencyApprovedWithDefferal> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(ApproveAgencyWithDefferal @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext;

            await this.Fire<SendEmailOnAgencyApprovedWithDefferal>(EventCondition, serviceBusContext);
        }
    }
}