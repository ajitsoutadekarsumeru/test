using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class SendEmailForPrimaryAllocationFailed : ISendEmailForPrimaryAllocationFailed
    {
        protected readonly ILogger<SendEmailForPrimaryAllocationFailed> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;

        public SendEmailForPrimaryAllocationFailed(ILogger<SendEmailForPrimaryAllocationFailed> logger,
            IRepoFactory repoFactory, ISmsUtility smsUtility, IEmailUtility emailUtility, MessageTemplateFactory messageTemplateFactory)
        {
            _smsUtility = smsUtility;
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(PrimaryAllocationFailedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _logger.LogInformation("SendEmailForPrimaryAllocationFailed : Start");
            _flexAppContext = @event.AppContext; //do not remove this line
            var repo = _repoFactory.Init(@event);
            string tenantId = _flexAppContext.TenantId;

            var model = await repo.GetRepo().FindAll<PrimaryAllocationFile>().Where(x => x.Id == @event.Id).FirstOrDefaultAsync();

            var userEmail = await repo.GetRepo().FindAll<ApplicationUser>().Where(x => x.Id == model.CreatedBy).Select(a => a.PrimaryEMail).FirstOrDefaultAsync();

            //ServiceLayerTemplate
            _logger.LogInformation("SendEmailForPrimaryAllocationFailed : MessageTemplate - " + @event.FileType.ToLower());
            IMessageTemplate? messageTemplate = null;

            if (string.Equals(@event.FileType, "agency", StringComparison.OrdinalIgnoreCase))
            {
                messageTemplate = _messageTemplateFactory.PrimaryAllocationAgencyFailedTemplate(model.CustomId, tenantId);
            }
            else if (string.Equals(@event.FileType, "telecallingagency", StringComparison.OrdinalIgnoreCase))
            {
                messageTemplate = _messageTemplateFactory.PrimaryAllocationTCAgencyFailedTemplate(model.CustomId, tenantId);
            }
            else if (string.Equals(@event.FileType, "allocationowner", StringComparison.OrdinalIgnoreCase))
            {
                messageTemplate = _messageTemplateFactory.PrimaryAllocationOwnerFailedTemplate(model.CustomId, tenantId);
            }

            _logger.LogInformation("SendEmailForPrimaryAllocationFailed : Send Email - " + userEmail);
            await _emailUtility.SendEmailAsync(userEmail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, tenantId);

            _logger.LogInformation("SendEmailForPrimaryAllocationFailed - : End");
        }
    }
}