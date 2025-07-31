using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class SendEMailForCollectionAdded : ISendEMailForCollectionAdded
    {
        protected readonly ILogger<SendEMailForCollectionAdded> _logger;
        protected string EventCondition = "";
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;

        public SendEMailForCollectionAdded(ILogger<SendEMailForCollectionAdded> logger,
            IRepoFactory repoFactory, ISmsUtility smsUtility, IEmailUtility emailUtility, MessageTemplateFactory messageTemplateFactory)
        {
            _smsUtility = smsUtility;
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(CollectionAddedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _logger.LogInformation("SendEMailForCollectionAdded : Start");
            _flexAppContext = @event.AppContext;
            _repoFactory.Init(@event);

            var collectionDto = await _repoFactory.GetRepo().FindAll<Collection>().ByCollectionId(@event.Id).SelectTo<CollectionDtoWithId>().FirstOrDefaultAsync();

            collectionDto.SetAppContext(@event.AppContext);
            var messageTemplate = _messageTemplateFactory.IssueReceiptNotificationTemplate(collectionDto);

            _logger.LogInformation("SendEMailForCollectionAdded : Email - " + collectionDto.EMailId);
            await _emailUtility.SendEmailAsync(collectionDto.EMailId, messageTemplate.EmailMessage, messageTemplate.EmailSubject, @event.AppContext.TenantId);

            _logger.LogInformation("SendEMailForCollectionAdded : End");
        }
    }
}