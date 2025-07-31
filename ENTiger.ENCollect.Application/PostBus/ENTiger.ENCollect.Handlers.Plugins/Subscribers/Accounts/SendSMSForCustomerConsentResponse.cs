using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class SendSMSForCustomerConsentResponse : ISendSMSForCustomerConsentResponse
    {
        protected readonly ILogger<SendSMSForCustomerConsentResponse> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;

        public SendSMSForCustomerConsentResponse(ILogger<SendSMSForCustomerConsentResponse> logger, IRepoFactory repoFactory, MessageTemplateFactory messageTemplateFactory, ISmsUtility smsUtility)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _messageTemplateFactory = messageTemplateFactory;
            _smsUtility = smsUtility;
        }

        public virtual async Task Execute(CustomerConsentUpdated @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; //do not remove this line
            var _tenantId = @event.AppContext.TenantId;
            _repoFactory.Init(@event);
            _logger.LogInformation("SendSMSForCustomerConsentResponse : Start");

            var consent = await _repoFactory.GetRepo().FindAll<CustomerConsent>()
                .ByConsentId(@event.ConsentId)
                .IncludeAccount()
                .IncludeApplicationUser()
                .FirstOrDefaultAsync();

            if (consent?.Account != null && consent.User != null)
            {
                CustomerConsentResponseMessageDto message = new CustomerConsentResponseMessageDto();
                message.Status = @event.Status;
                message.Date = @event.AppointmentDate;
                message.AccountNo = consent.Account.AGREEMENTID;
                message.ClientName = consent.Account.CUSTOMERNAME ?? "";
                message.UserName = consent.User.FirstName + " " + consent.User.LastName ?? "";
                //ServiceLayerTemplate
                var messageTemplate = _messageTemplateFactory.CustomerConsentResponseTemplate(message);

                _logger.LogInformation($"SendSMSForCustomerConsentResponse : SMS - {consent.User.PrimaryMobileNumber ?? "none"}");
                if (!String.IsNullOrEmpty(consent.User.PrimaryMobileNumber))
                {
                    await _smsUtility.SendSMS(consent.User.PrimaryMobileNumber, messageTemplate.SMSMessage, @event.AppContext.TenantId);
                }
            }
            _logger.LogInformation("SendSMSForCustomerConsentResponse : End");
        }
    }
}
