using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect.Utilities.UserManagement.Staff
{
    public class CompanyUserDormantEmailNotification : IMessageTemplate, IFlexUtilityService
    {
        public string EmailSubject { get; set; } = string.Empty;
        public string EmailMessage { get; set; } = string.Empty;
        public string SMSMessage { get; set; } = string.Empty;

        private readonly NotificationSettings _notificationSettings;

        public CompanyUserDormantEmailNotification(IOptions<NotificationSettings> notificationSettings)
        {
            _notificationSettings = notificationSettings.Value;
        }

        public virtual void ConstructData(CompanyUserDto _dto, int InactiveDays)
        {
            string EmailSignature = _notificationSettings.EmailSignature;
            string UserName = _dto.FirstName + " " + _dto.LastName;
            EmailMessage = $"Hi {UserName},<br><br>We noticed that you haven’t logged into your account for over <b>{InactiveDays} days</b>. " +
                "To help keep our system secure, your account has been marked as <b>Dormant</b> due to this period of inactivity.<br><br>" +
                "As a result, your access has been temporarily disabled.<br><br>If you’d like to use the system again, just reach out to your <b>supervisor</b>, and will help you get reactivated.<br><br>" +
                $"Thanks,<br>{EmailSignature}<br>Support Team<br>(This is a system generated email kindly do not reply)<br>";

            EmailSubject = "Your account has been marked as dormant";
        }
    }
}
