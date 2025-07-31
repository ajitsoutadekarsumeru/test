using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class SendSMSForCollectionAdded : ISendSMSForCollectionAdded
    {
        protected readonly ILogger<SendSMSForCollectionAdded> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;

        public SendSMSForCollectionAdded(ILogger<SendSMSForCollectionAdded> logger,
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
            _logger.LogInformation("SendSMSForCollectionAdded : Start");
            _flexAppContext = @event.AppContext; //do not remove this line
            _repoFactory.Init(@event);

            var collectionDto = await _repoFactory.GetRepo().FindAll<Collection>()
                                        .ByCollectionId(@event.Id)
                                        .SelectTo<CollectionDtoWithId>()
                                        .FirstOrDefaultAsync();

            collectionDto.SetAppContext(@event.AppContext);
            var messageTemplate = _messageTemplateFactory.IssueReceiptSMSNotificationTemplate(collectionDto);

            _logger.LogInformation("SendSMSForCollectionAdded : SMS - " + collectionDto.MobileNo);
            await _smsUtility.SendSMS(collectionDto.MobileNo, messageTemplate.SMSMessage, @event.AppContext.TenantId);

            _logger.LogInformation("SendSMSForCollectionAdded : End");
        }
    }
}