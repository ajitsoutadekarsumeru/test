using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.PublicModule
{
    public partial class SendEmailForImportAccountsUploaded : ISendEmailForImportAccountsUploaded
    {
        protected readonly ILogger<SendEmailForImportAccountsUploaded> _logger;
        protected string EventCondition = "";
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;

        public SendEmailForImportAccountsUploaded(ILogger<SendEmailForImportAccountsUploaded> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(ImportAccountsUploadedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext;

            await this.Fire<SendEmailForImportAccountsUploaded>(EventCondition, serviceBusContext);
        }
    }
}