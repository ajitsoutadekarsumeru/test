using Sumeru.Flex;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class SendEmailForCustomerConsent : ISendEmailForCustomerConsent
    {
        protected readonly ILogger<SendEmailForCustomerConsent> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected readonly IEmailUtility _emailUtility;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly MessageTemplateFactory _messageTemplateFactory;

        public SendEmailForCustomerConsent(ILogger<SendEmailForCustomerConsent> logger, IRepoFactory repoFactory, IEmailUtility emailUtility, MessageTemplateFactory messageTemplateFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
        }

        public virtual async Task Execute(CustomerConsentRequested @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; //do not remove this line
 
            _logger.LogInformation("SendEmailForCustomerConsent : Start");
            _repoFactory.Init(@event);
            var _tenantId = @event.AppContext.TenantId;
            var consent = await _repoFactory.GetRepo().FindAll<CustomerConsent>()
                .ByConsentId(@event.ConsentId)
                .IncludeAccount()
                .IncludeApplicationUser()
                .FirstOrDefaultAsync();

            if (consent.Account != null)
            {
                CustomerConsentMessageDto message = new CustomerConsentMessageDto();
                message.ClientName = consent.Account.Lender?.Name ?? "";
                message.Date = @event.AppointmentDate;
                message.Link = @event.Link;
                //ServiceLayerTemplate
                var messageTemplate = _messageTemplateFactory.CustomerConsentTemplate(message);
                _logger.LogInformation($"SendEmailForCustomerConsent : Email - {consent.Account.EMAIL_ID ?? "none"}");
                if (!String.IsNullOrEmpty(consent.Account.EMAIL_ID))
                {
                    await _emailUtility.SendEmailAsync(consent.Account.EMAIL_ID, messageTemplate.EmailMessage, messageTemplate.EmailSubject, _tenantId);
                }
            }
            _logger.LogInformation("SendEmailForCustomerConsent : End");
        }
    }
}
