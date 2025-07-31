using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public class SendEmailForCustomerConsentResponse : ISendEmailForCustomerConsentResponse
    {
        protected readonly ILogger<SendEmailForCustomerConsentResponse> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected readonly IEmailUtility _emailUtility;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly MessageTemplateFactory _messageTemplateFactory;

        public SendEmailForCustomerConsentResponse(ILogger<SendEmailForCustomerConsentResponse> logger, IRepoFactory repoFactory, IEmailUtility emailUtility, MessageTemplateFactory messageTemplateFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
        }

        public virtual async Task Execute(CustomerConsentUpdated @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; //do not remove this line

            _logger.LogInformation("SendEmailForCustomerConsentResponse : Start");
            _repoFactory.Init(@event);
            var _tenantId = @event.AppContext.TenantId;
            var consent = await _repoFactory.GetRepo().FindAll<CustomerConsent>()
                .ByConsentId(@event.ConsentId)
                .IncludeAccount()
                .IncludeApplicationUser()
                .FirstOrDefaultAsync();

            if (consent?.Account != null && consent.User != null)
            {
                CustomerConsentResponseMessageDto message = new CustomerConsentResponseMessageDto();
                message.ClientName = consent.Account.CUSTOMERNAME ?? "";
                message.Date = @event.AppointmentDate;
                message.Status = @event.Status;
                message.AccountNo = consent.Account.AGREEMENTID;
                message.UserName = consent.User.FirstName + " " + consent.User.LastName ?? "";
                //ServiceLayerTemplate
                var messageTemplate = _messageTemplateFactory.CustomerConsentResponseTemplate(message);
                _logger.LogInformation($"SendEmailForCustomerConsentResponse : Email - {consent.User.PrimaryEMail ?? "none"}");
                if (!String.IsNullOrEmpty(consent.User.PrimaryEMail))
                {
                    await _emailUtility.SendEmailAsync(consent.User.PrimaryEMail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, _tenantId);
                }
            }
            _logger.LogInformation("SendEmailForCustomerConsentResponse : End");
        }
    }
}
