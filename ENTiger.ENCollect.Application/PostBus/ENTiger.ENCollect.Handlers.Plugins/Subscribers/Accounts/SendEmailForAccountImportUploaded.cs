using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class SendEmailForAccountImportUploaded : ISendEmailForAccountImportUploaded
    {
        protected readonly ILogger<SendEmailForAccountImportUploaded> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;

        public SendEmailForAccountImportUploaded(ILogger<SendEmailForAccountImportUploaded> logger,
            IRepoFactory repoFactory, ISmsUtility smsUtility, IEmailUtility emailUtility, MessageTemplateFactory messageTemplateFactory)
        {
            _smsUtility = smsUtility;
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(AccountImportUploadedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _logger.LogInformation("SendEmailForAccountImportUploaded : Start : ");
            _flexAppContext = @event.AppContext; //do not remove this line
            var repo = _repoFactory.Init(@event);
            //TODO: Write your business logic here:
            var _tenantId = @event.AppContext.TenantId;
            var model = await repo.GetRepo().FindAll<MasterFileStatus>().Where(x => x.Id == @event.Id).FirstOrDefaultAsync();

            var user = await repo.GetRepo().FindAll<ApplicationUser>().Where(x => x.Id == model.CreatedBy).FirstOrDefaultAsync();

            //ServiceLayerTemplate
            var messageTemplate = _messageTemplateFactory.AccountImportReceivedTemplate(model.FileName, model.FilePath, _tenantId);

            _logger.LogInformation("SendEmailForAccountImportUploaded : Email - " + user.PrimaryEMail);
            await _emailUtility.SendEmailAsync(user.PrimaryEMail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, _tenantId);
            _logger.LogInformation("SendEmailForAccountImportUploaded : End");

            await Task.CompletedTask;
        }
    }
}