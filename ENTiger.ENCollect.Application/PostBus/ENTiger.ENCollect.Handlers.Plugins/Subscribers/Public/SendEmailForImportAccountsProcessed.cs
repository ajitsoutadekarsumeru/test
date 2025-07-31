using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.PublicModule
{
    public partial class SendEmailForImportAccountsProcessed : ISendEmailForImportAccountsProcessed
    {
        protected readonly ILogger<SendEmailForImportAccountsProcessed> _logger;
        protected string EventCondition = "";
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;

        public SendEmailForImportAccountsProcessed(ILogger<SendEmailForImportAccountsProcessed> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(ImportAccountsProcessedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext;

            await this.Fire<SendEmailForImportAccountsProcessed>(EventCondition, serviceBusContext);
        }
    }
}