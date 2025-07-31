using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class SendEmailForMastersImportUploaded : ISendEmailForMastersImportUploaded
    {
        protected readonly ILogger<SendEmailForMastersImportUploaded> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;

        public SendEmailForMastersImportUploaded(ILogger<SendEmailForMastersImportUploaded> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(MastersImportUploadedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext;

            await this.Fire<SendEmailForMastersImportUploaded>(EventCondition, serviceBusContext);
        }
    }
}