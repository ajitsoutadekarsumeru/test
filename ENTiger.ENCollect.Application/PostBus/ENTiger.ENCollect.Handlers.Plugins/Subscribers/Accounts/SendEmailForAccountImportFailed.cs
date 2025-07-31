using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class SendEmailForAccountImportFailed : ISendEmailForAccountImportFailed
    {
        protected readonly ILogger<SendEmailForAccountImportFailed> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;

        public SendEmailForAccountImportFailed(ILogger<SendEmailForAccountImportFailed> logger,
            IRepoFactory repoFactory, ISmsUtility smsUtility, IEmailUtility emailUtility, MessageTemplateFactory messageTemplateFactory)
        {
            _smsUtility = smsUtility;
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(AccountImportFailedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _logger.LogInformation("SendEmailForAccountImportFailed : Start : ");
            _flexAppContext = @event.AppContext; //do not remove this line
            var repo = _repoFactory.Init(@event);
            //TODO: Write your business logic here:
            var _tenantId = @event.AppContext.TenantId;
            var model = await repo.GetRepo().FindAll<MasterFileStatus>().Where(x => x.Id == @event.Id).FirstOrDefaultAsync();

            var user = await repo.GetRepo().FindAll<ApplicationUser>().Where(x => x.Id == model.CreatedBy).FirstOrDefaultAsync();

            //ServiceLayerTemplate
            var messageTemplate = _messageTemplateFactory.AccountImportFailedTemplate(@event.Remarks, _tenantId);

            List<string> files = new List<string>() { @event.FileName };

            _logger.LogInformation("SendEmailForAccountImportFailed : Email - " + user.PrimaryEMail);
            await _emailUtility.SendEmailAsync(user.PrimaryEMail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, _tenantId,files,model.FilePath);
            _logger.LogInformation("SendEmailForAccountImportFailed : End");

            await Task.CompletedTask;
        }
    }
}