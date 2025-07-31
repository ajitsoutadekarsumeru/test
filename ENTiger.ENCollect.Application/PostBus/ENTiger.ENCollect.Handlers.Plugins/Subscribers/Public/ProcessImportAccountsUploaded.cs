using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.PublicModule
{
    public partial class ProcessImportAccountsUploaded : IProcessImportAccountsUploaded
    {
        protected readonly ILogger<ProcessImportAccountsUploaded> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;

        public ProcessImportAccountsUploaded(ILogger<ProcessImportAccountsUploaded> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(ImportAccountsUploadedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; 
            await this.Fire<ProcessImportAccountsUploaded>(EventCondition, serviceBusContext);
        }
    }
}