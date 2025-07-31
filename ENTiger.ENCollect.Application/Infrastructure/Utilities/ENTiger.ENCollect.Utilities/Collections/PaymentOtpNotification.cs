using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class PaymentOtpNotification : IMessageTemplate, IFlexUtilityService
    {
        public virtual string EmailSubject { get; set; } = string.Empty;
        public virtual string EmailMessage { get; set; } = string.Empty;
        public virtual string SMSMessage { get; set; } = string.Empty;

        private readonly NotificationSettings _notificationSettings;

        public PaymentOtpNotification(IOptions<NotificationSettings> notificationSettings)
        {
            _notificationSettings = notificationSettings.Value;
        }

        public virtual void ConstructData(string OTP)
        {
            //SendPaymentOTPService
            string SMSSignature = _notificationSettings.SmsSignature;

            SMSMessage = "OTP Verification Number: "
                + OTP + ". Do not share this with anyone - " + SMSSignature + "";
        }
    }
}