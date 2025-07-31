using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class SendEmailForCollectionCancellation : ISendEmailForCollectionCancellation
    {
        protected readonly ILogger<SendEmailForCollectionCancellation> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;

        public SendEmailForCollectionCancellation(ILogger<SendEmailForCollectionCancellation> logger,
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

            _logger.LogTrace("SendEmailForCollectionCancellation : Sending Email - " + collectionDto.EMailId);
            await _emailUtility.SendEmailAsync(collectionDto.EMailId, messageTemplate.EmailMessage, messageTemplate.EmailSubject, @event.AppContext.TenantId);
            _logger.LogTrace("SendEmailForCollectionCancellation : Email sent successfully.");

            await this.Fire<SendEmailForCollectionCancellation>(EventCondition, serviceBusContext);
        }
    }
}