using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class VerifyAccountNotification : IMessageTemplate, IFlexUtilityService
    {
        public virtual string EmailSubject { get; set; } = string.Empty;
        public virtual string EmailMessage { get; set; } = string.Empty;
        public virtual string SMSMessage { get; set; } = string.Empty;
        private readonly NotificationSettings _notificationSettings;

        private readonly OtpSettings _otpSettings;

        public VerifyAccountNotification(IOptions<NotificationSettings> notificationSettings, IOptions<OtpSettings> otpSettings)
        {
            _notificationSettings = notificationSettings.Value;
            _otpSettings = otpSettings.Value;
        }

        public virtual void ConstructData(string OTP)
        {
            string SMSSignature = _notificationSettings.SmsSignature;
            int OTPExpiry = _otpSettings.Expiry.PaymentPluginOtpInMins;

            SMSMessage = "Your OTP for payment plugin is " + OTP + ". Valid for "
                + OTPExpiry + " minutes. Thanks " + SMSSignature + "";
        }
    }
}