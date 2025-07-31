using System.Threading.Tasks;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class UpdateConsentRequestAuditTrail : IUpdateConsentRequestAuditTrail
    {
        protected readonly ILogger<UpdateConsentRequestAuditTrail> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        public UpdateConsentRequestAuditTrail(ILogger<UpdateConsentRequestAuditTrail> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(CustomerConsentRequested @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; 

            await this.Fire<UpdateConsentRequestAuditTrail>(EventCondition, serviceBusContext);
        }
    }
}
