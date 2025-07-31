using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class SendSMSOnCompanyUserApproved : ISendSMSOnCompanyUserApproved
    {
        protected readonly ILogger<SendSMSOnCompanyUserApproved> _logger;
        protected string EventCondition = "";
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;

        public SendSMSOnCompanyUserApproved(ILogger<SendSMSOnCompanyUserApproved> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(CompanyUserApproved @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; 
            await this.Fire<SendSMSOnCompanyUserApproved>(EventCondition, serviceBusContext);
        }
    }
}