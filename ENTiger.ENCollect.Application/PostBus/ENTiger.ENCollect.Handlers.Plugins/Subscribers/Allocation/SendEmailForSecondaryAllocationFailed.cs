using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class SendEmailForSecondaryAllocationFailed : ISendEmailForSecondaryAllocationFailed
    {
        protected readonly ILogger<SendEmailForSecondaryAllocationFailed> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;

        public SendEmailForSecondaryAllocationFailed(ILogger<SendEmailForSecondaryAllocationFailed> logger,
            IRepoFactory repoFactory, ISmsUtility smsUtility, IEmailUtility emailUtility, MessageTemplateFactory messageTemplateFactory)
        {
            _smsUtility = smsUtility;
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(SecondaryAllocationFailedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _logger.LogInformation("SendEmailForSecondaryAllocationFailed : Start");
            _flexAppContext = @event.AppContext; //do not remove this line
            var repo = _repoFactory.Init(@event);
            string tenantId = _flexAppContext.TenantId;

            var model = await repo.GetRepo().FindAll<SecondaryAllocationFile>().Where(x => x.Id == @event.Id).FirstOrDefaultAsync();

            var userEmail = await repo.GetRepo().FindAll<ApplicationUser>().Where(x => x.Id == model.CreatedBy).Select(a => a.PrimaryEMail).FirstOrDefaultAsync();

            //ServiceLayerTemplate
            IMessageTemplate? messageTemplate = null;
            _logger.LogInformation("SendEmailForSecondaryAllocationFailed : MessageTemplate - " + @event.FileType);

            if (string.Equals(@event.FileType, "agent", StringComparison.OrdinalIgnoreCase))
            {
                messageTemplate = _messageTemplateFactory.SecondaryAllocationAgentFailedTemplate(model.CustomId, tenantId);
            }
            else if (string.Equals(@event.FileType, "staff", StringComparison.OrdinalIgnoreCase))
            {
                messageTemplate = _messageTemplateFactory.SecondaryAllocationStaffFailedTemplate(model.CustomId, tenantId);
            }
            else if (string.Equals(@event.FileType, "telecaller", StringComparison.OrdinalIgnoreCase))
            {
                messageTemplate = _messageTemplateFactory.SecondaryAllocationTeleCallerFailedTemplate(model.CustomId, tenantId);
            }


            _logger.LogInformation("SendEmailForSecondaryAllocationFailed : Send Email : " + userEmail);
            await _emailUtility.SendEmailAsync(userEmail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, tenantId);

            _logger.LogInformation("SendEmailForSecondaryAllocationFailed : End");
        }
    }
}