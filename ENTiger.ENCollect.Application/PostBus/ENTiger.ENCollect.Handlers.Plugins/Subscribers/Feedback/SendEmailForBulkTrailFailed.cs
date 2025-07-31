using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.FeedbackModule
{
    public partial class SendEmailForBulkTrailFailed : ISendEmailForBulkTrailFailed
    {
        protected readonly ILogger<SendEmailForBulkTrailFailed> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;

        public SendEmailForBulkTrailFailed(ILogger<SendEmailForBulkTrailFailed> logger, IRepoFactory repoFactory, ISmsUtility smsUtility, IEmailUtility emailUtility, MessageTemplateFactory messageTemplateFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _smsUtility = smsUtility;
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
        }

        public virtual async Task Execute(BulkTrailFailedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; //do not remove this line
            _repoFactory.Init(@event);
            //TODO: Write your business logic here:

            _logger.LogInformation("BulkTrailFaileService : Start");

            var bulkTraildto = await _repoFactory.GetRepo().FindAll<BulkTrailUploadFile>()
                                            .Where(a => a.Id == @event.Id)
                                            .SelectTo<BulkTrailUploadFileDtoWithId>()
                                            .FirstOrDefaultAsync();

            var userEmail = await _repoFactory.GetRepo().FindAll<ApplicationUser>()
                                            .Where(x => x.Id == bulkTraildto.CreatedBy)
                                            .Select(a => a.PrimaryEMail).FirstOrDefaultAsync();

            var messageTemplate = _messageTemplateFactory.BulkTrailUploadFailedEmailTemplate(bulkTraildto);

            _logger.LogInformation("SendEMailOBulkTrailUploadFailed : " + bulkTraildto.CustomId);
            await _emailUtility.SendEmailAsync(userEmail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, @event.AppContext.TenantId);
        }
    }
}