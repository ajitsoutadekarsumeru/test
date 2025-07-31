using Sumeru.Flex;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class SendSMSForCustomerConsent : ISendSMSForCustomerConsent
    {
        protected readonly ILogger<SendSMSForCustomerConsent> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;

        public SendSMSForCustomerConsent(ILogger<SendSMSForCustomerConsent> logger, IRepoFactory repoFactory, MessageTemplateFactory messageTemplateFactory, ISmsUtility smsUtility)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _messageTemplateFactory = messageTemplateFactory;
            _smsUtility = smsUtility;
        }

        public virtual async Task Execute(CustomerConsentRequested @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; //do not remove this line
            var _tenantId = @event.AppContext.TenantId;
            _repoFactory.Init(@event);
            _logger.LogInformation("SendSMSForCustomerConsent : Start");
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

                _logger.LogInformation($"SendSMSForCustomerConsent : SMS - {consent.Account.MAILINGMOBILE ?? "none"}");
                if (!String.IsNullOrEmpty(consent.Account.MAILINGMOBILE))
                {
                    await _smsUtility.SendSMS(consent.Account.MAILINGMOBILE, messageTemplate.SMSMessage, @event.AppContext.TenantId);
                }
            }
            _logger.LogInformation("SendSMSForCustomerConsent : End");
        }
    }
}
