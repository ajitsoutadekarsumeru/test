using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class SendEmailForCreateUsersUploaded : ISendEmailForCreateUsersUploaded
    {
        protected readonly ILogger<SendEmailForCreateUsersUploaded> _logger;
        protected string EventCondition = "";
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;

        public SendEmailForCreateUsersUploaded(ILogger<SendEmailForCreateUsersUploaded> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(CreateUsersUploadedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; //do not remove this line
            var repo = _repoFactory.Init(@event);
            await this.Fire<SendEmailForCreateUsersUploaded>(EventCondition, serviceBusContext);
        }
    }
}