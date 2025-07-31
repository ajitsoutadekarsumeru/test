using Sumeru.Flex;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class SendEmailForCollectionBulkUploadFailed : ISendEmailForCollectionBulkUploadFailed
    {
        protected readonly ILogger<SendEmailForCollectionBulkUploadFailed> _logger;
        protected string EventCondition = "";
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IEmailUtility _emailUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;
        public SendEmailForCollectionBulkUploadFailed(
            ILogger<SendEmailForCollectionBulkUploadFailed> logger, 
            IRepoFactory repoFactory, 
            IEmailUtility emailUtility,
            MessageTemplateFactory messageTemplateFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _emailUtility = emailUtility; 
            _messageTemplateFactory = messageTemplateFactory;
        }

        public virtual async Task Execute(CollectionBulkUploadFailedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; 
            _repoFactory.Init(@event);
           

            _logger.LogInformation("SendEmailForCollectionBulkUploadFailed : Start");

            var bulkUploadDto = await _repoFactory.GetRepo()
                .FindAll<CollectionUploadFile>()
                .Where(a => a.Id == @event.Id)
                .SelectTo<CollectionUploadFileDtoWithId>()
                .FirstOrDefaultAsync();

            var userEmail = await _repoFactory.GetRepo().FindAll<ApplicationUser>()
                                    .Where(x => x.Id == bulkUploadDto.CreatedBy)
                                    .Select(a => a.PrimaryEMail)
                                    .FirstOrDefaultAsync();

            var messageTemplate = _messageTemplateFactory.CollectionBulkUploadFailedEmailTemplate(bulkUploadDto);

            _logger.LogInformation("SendEmailForCollectionBulkUploadFailed : " + bulkUploadDto.CustomId);
            await _emailUtility.SendEmailAsync(userEmail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, @event.AppContext.TenantId);

            await this.Fire<SendEmailForCollectionBulkUploadFailed>(EventCondition, serviceBusContext);
        }
    }
}
