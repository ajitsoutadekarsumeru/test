using ENTiger.ENCollect.AccountsModule;
using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class AccountPaymentNotification : IMessageTemplate, IFlexUtilityService
    {
        public virtual string EmailSubject { get; set; } = string.Empty;
        public virtual string EmailMessage { get; set; } = string.Empty;
        public virtual string SMSMessage { get; set; } = string.Empty;

        private readonly NotificationSettings _notificationSettings;
        private readonly PaymentSettings _paymentSettings;
        public AccountPaymentNotification(IOptions<NotificationSettings> notificationSettings, IOptions<PaymentSettings> paymentSettings)
        {
            _notificationSettings = notificationSettings.Value;
            _paymentSettings = paymentSettings.Value;
        }

        public virtual void ConstructData(AccountMessageDto dto)
        {
            string SMSSignature = _notificationSettings.SmsSignature;
            string staticPaymentURL = _paymentSettings.StaticPaymentLink;

            SMSMessage = "Dear Customer, You can directly visit us on link " + staticPaymentURL +
                            " and make direct payment to your account. Thanks, " + SMSSignature + "";
        }
    }
}