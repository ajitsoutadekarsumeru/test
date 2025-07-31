using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class SendEmailForCollectionCancellationRejected : ISendEmailForCollectionCancellationRejected
    {
        protected readonly ILogger<SendEmailForCollectionCancellationRejected> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;

        public SendEmailForCollectionCancellationRejected(ILogger<SendEmailForCollectionCancellationRejected> logger,
            IRepoFactory repoFactory, ISmsUtility smsUtility, IEmailUtility emailUtility, MessageTemplateFactory messageTemplateFactory)
        {
            _smsUtility = smsUtility;
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(CollectionCancellationRejected @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext;
            var repo = _repoFactory.Init(@event);

            var collectionDto = await repo.GetRepo().FindAll<Collection>()
                                  .ByCollectionId(@event.Id)
                                  .SelectTo<CollectionDtoWithId>()
                                  .FirstOrDefaultAsync();

            collectionDto.SetAppContext(_flexAppContext);
            var messageTemplate = _messageTemplateFactory.CollectionCancellationRejectedTemplate(collectionDto, @event.AppContext.TenantId);

            _logger.LogTrace("SendEmailForCollectionCancellationRejected : Sending Email - " + collectionDto.EMailId);
            await _emailUtility.SendEmailAsync(collectionDto.EMailId, messageTemplate.EmailMessage, messageTemplate.EmailSubject, @event.AppContext.TenantId);
            _logger.LogTrace("SendEmailForCollectionCancellationRejected : Email sent successfully.");

            await this.Fire<SendEmailForCollectionCancellationRejected>(EventCondition, serviceBusContext);
        }
    }
}