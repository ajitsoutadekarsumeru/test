using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class UpdateConsentResponseAuditTrail : IUpdateConsentResponseAuditTrail
    {
        protected readonly ILogger<UpdateConsentResponseAuditTrail> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        public UpdateConsentResponseAuditTrail(ILogger<UpdateConsentResponseAuditTrail> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(CustomerConsentUpdated @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext;
            await this.Fire<UpdateConsentResponseAuditTrail>(EventCondition, serviceBusContext);
        }
    }
}
