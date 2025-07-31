using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect.Utilities.RegisterDevice
{
    public class RegisterDeviceSendOTPSMSNotification : IMessageTemplate, IFlexUtilityService
    {
        public virtual string EmailSubject { get; set; } = string.Empty;
        public virtual string EmailMessage { get; set; } = string.Empty;
        public virtual string SMSMessage { get; set; } = string.Empty;
        private readonly NotificationSettings _notificationSettings;

        public RegisterDeviceSendOTPSMSNotification(IOptions<NotificationSettings> notificationSettings)
        {
            _notificationSettings = notificationSettings.Value;
        }

        public virtual void ConstructData(DeviceDetailDtoWithId dto)
        {
            string SMSSignature = _notificationSettings.SmsSignature;
            SMSMessage = "Your OTP for device registration is: " + dto.OTP + ". " + SMSSignature + "";
        }
    }
}