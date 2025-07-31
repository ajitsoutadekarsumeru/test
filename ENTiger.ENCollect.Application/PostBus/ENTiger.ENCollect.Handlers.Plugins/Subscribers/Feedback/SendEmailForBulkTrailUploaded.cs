using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.FeedbackModule
{
    public partial class SendEmailForBulkTrailUploaded : ISendEmailForBulkTrailUploaded
    {
        protected readonly ILogger<SendEmailForBulkTrailUploaded> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;
        private string TenantId = string.Empty;

        public SendEmailForBulkTrailUploaded(ILogger<SendEmailForBulkTrailUploaded> logger, IRepoFactory repoFactory, ISmsUtility smsUtility, IEmailUtility emailUtility, MessageTemplateFactory messageTemplateFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _smsUtility = smsUtility;
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
        }

        public virtual async Task Execute(BulkTrailUploadedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; //do not remove this line
            _repoFactory.Init(@event);
            TenantId = _flexAppContext.TenantId;
            //TODO: Write your business logic here:
            _logger.LogInformation("BulkTrailUploadedService : Start");

            var bulkTraildto = await _repoFactory.GetRepo().FindAll<BulkTrailUploadFile>()
                                        .Where(a => a.CustomId == @event.CustomId)
                                        .SelectTo<BulkTrailUploadFileDtoWithId>()
                                        .FirstOrDefaultAsync();

            var messageTemplate = _messageTemplateFactory.BulkTrailUploadedEmailTemplate(bulkTraildto);

            var userEmail = await _repoFactory.GetRepo().FindAll<ApplicationUser>()
                                        .Where(x => x.Id == bulkTraildto.CreatedBy)
                                        .Select(a => a.PrimaryEMail).FirstOrDefaultAsync();

            _logger.LogInformation("SendEMailOBulkTrailUploaded : " + bulkTraildto.CustomId);
            await _emailUtility.SendEmailAsync(userEmail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, @event.AppContext.TenantId);

            await this.Fire<SendEmailForBulkTrailUploaded>(EventCondition, serviceBusContext);
        }
    }
}