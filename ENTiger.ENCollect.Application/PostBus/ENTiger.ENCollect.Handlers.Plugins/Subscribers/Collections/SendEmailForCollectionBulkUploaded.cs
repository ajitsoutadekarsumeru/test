using System.Threading.Tasks;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class SendEmailForCollectionBulkUploaded : ISendEmailForCollectionBulkUploaded
    {
        protected readonly ILogger<SendEmailForCollectionBulkUploaded> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IEmailUtility _emailUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;
        public SendEmailForCollectionBulkUploaded(
            ILogger<SendEmailForCollectionBulkUploaded> logger, 
            IRepoFactory repoFactory, 
            IEmailUtility emailUtility, 
            MessageTemplateFactory messageTemplateFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
        }

        public virtual async Task Execute(CollectionBulkUploadedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; //do not remove this line
            var repo = _repoFactory.Init(@event);
           
            var TenantId = _flexAppContext.TenantId;

            var bulkUploadDto = await _repoFactory.GetRepo()
                .FindAll<CollectionUploadFile>()
                .Where(a => a.Id == @event.Id)
                .SelectTo<CollectionUploadFileDtoWithId>()
                .FirstOrDefaultAsync();

            var messageTemplate = _messageTemplateFactory.CollectionBulkUploadedEmailTemplate(bulkUploadDto);

            var userEmail = await _repoFactory.GetRepo().FindAll<ApplicationUser>()
                                        .Where(x => x.Id == bulkUploadDto.CreatedBy)
                                        .Select(a => a.PrimaryEMail)
                                        .FirstOrDefaultAsync();

            _logger.LogInformation("SendEmailForCollectionBulkUploaded : " + bulkUploadDto.Id);
            await _emailUtility.SendEmailAsync(userEmail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, @event.AppContext.TenantId);
            
            await this.Fire<SendEmailForCollectionBulkUploaded>(EventCondition, serviceBusContext);
        }
    }
}
