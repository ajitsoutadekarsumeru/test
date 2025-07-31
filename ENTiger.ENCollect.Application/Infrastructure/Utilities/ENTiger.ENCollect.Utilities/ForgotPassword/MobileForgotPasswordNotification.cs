using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class MobileForgotPasswordNotification : IMessageTemplate, IFlexUtilityService
    {
        public virtual string EmailSubject { get; set; } = string.Empty;
        public virtual string EmailMessage { get; set; } = string.Empty;
        public virtual string SMSMessage { get; set; } = string.Empty;

        private readonly NotificationSettings _notificationSettings;

        public MobileForgotPasswordNotification(IOptions<NotificationSettings> notificationSettings)
        {
            _notificationSettings = notificationSettings.Value;
        }

        public virtual void ConstructData(SendMobileForgotPasswordDto dto)
        {
            string SMSSignature = _notificationSettings.SmsSignature;

            EmailMessage = "Your OTP for forgot password is: " + dto.Code + ".<br/>";

            EmailSubject = "Reset Password";

            SMSMessage = "Your OTP for forgot password is: " + dto.Code + ". " + SMSSignature + "";
        }
    }
}