using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.PublicModule
{
    public partial class SendEmailForImportAccountsFailed : ISendEmailForImportAccountsFailed
    {
        protected readonly ILogger<SendEmailForImportAccountsFailed> _logger;
        protected string EventCondition = "";
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;

        public SendEmailForImportAccountsFailed(ILogger<SendEmailForImportAccountsFailed> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(ImportAccountsFailedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; 
            await this.Fire<SendEmailForImportAccountsFailed>(EventCondition, serviceBusContext);
        }
    }
}