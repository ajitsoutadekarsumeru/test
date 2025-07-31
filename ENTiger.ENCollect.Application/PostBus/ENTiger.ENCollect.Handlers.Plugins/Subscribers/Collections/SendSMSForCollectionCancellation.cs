using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class SendSMSForCollectionCancellation : ISendSMSForCollectionCancellation
    {
        protected readonly ILogger<SendSMSForCollectionCancellation> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;

        public SendSMSForCollectionCancellation(ILogger<SendSMSForCollectionCancellation> logger,
            IRepoFactory repoFactory, ISmsUtility smsUtility, IEmailUtility emailUtility, MessageTemplateFactory messageTemplateFactory)
        {
            _smsUtility = smsUtility;
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(CollectionCancellationAdded @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext;
            var repo = _repoFactory.Init(@event);

            var collectionDto = await repo.GetRepo().FindAll<Collection>()
                                   .ByCollectionId(@event.Id)
                                   .SelectTo<CollectionDtoWithId>()
                                   .FirstOrDefaultAsync();
            collectionDto.SetAppContext(_flexAppContext);

            var messageTemplate = _messageTemplateFactory.CollectionCancellationRequestedTemplate(collectionDto, @event.AppContext.TenantId);

            _logger.LogInformation("SendSMSForCollectionCancellation : SMS - " + collectionDto.MobileNo);

            await _smsUtility.SendSMS(collectionDto.MobileNo, messageTemplate.SMSMessage, @event.AppContext.TenantId);
            _logger.LogInformation("SendSMSForCollectionCancellation : Send SMS - End");

            await this.Fire<SendSMSForCollectionCancellation>(EventCondition, serviceBusContext);
        }
    }
}