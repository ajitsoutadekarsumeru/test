using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class LoginOTPNotification : IMessageTemplate, IFlexUtilityService
    {
        public virtual string EmailSubject { get; set; } = string.Empty;
        public virtual string EmailMessage { get; set; } = string.Empty;
        public virtual string SMSMessage { get; set; } = string.Empty;

        private readonly NotificationSettings _notificationSettings;
        private readonly OtpSettings _otpSettings;

        public LoginOTPNotification(IOptions<NotificationSettings> notificationSettings, IOptions<OtpSettings> otpSettings)
        {
            _notificationSettings = notificationSettings.Value;
            _otpSettings = otpSettings.Value;
        }

        public virtual void ConstructData(SendLoginOTPDto _dto)
        {
            string SMSSignature = _notificationSettings.SmsSignature;
            int loginOTPExpiredTime = _otpSettings.Expiry.LoginOtpInMins;
            SMSMessage = "Your OTP for login is: " + _dto.Otp + " It is valid for " + loginOTPExpiredTime + " minutes only. " + SMSSignature + "";
            EmailMessage = "<p>Dear " + _dto.FirstName + " " + _dto.LastName + "</p><br/>" +
                $"Your one time password for secured login to your account is " + _dto.Otp + ". Do not share this OTP with anyone.</p><br/>";
            EmailSubject = "OTP for your application";
        }
    }

    #region ClientClass

    //public class Client1SendOtp : SendLoginOTP
    //{
    //    public Client1SendOtp(SendLoginOTPDto dto) : base(dto)
    //    {
    //    }

    //    public override void ConstructData()
    //    {
    //        base.ConstructData();
    //        EmailMessage += "\nThis is an additional message for Client 1.";
    //    }
    //}

    #endregion ClientClass
}