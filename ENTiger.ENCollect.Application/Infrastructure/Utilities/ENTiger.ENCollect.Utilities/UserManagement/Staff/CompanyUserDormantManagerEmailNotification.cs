using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect.Utilities.UserManagement.Staff
{
    public class CompanyUserDormantManagerEmailNotification : IMessageTemplate, IFlexUtilityService
    {
        public string EmailSubject { get; set; } = string.Empty;
        public string EmailMessage { get; set; } = string.Empty;
        public string SMSMessage { get; set; } = string.Empty;

        private readonly NotificationSettings _notificationSettings;

        public CompanyUserDormantManagerEmailNotification(IOptions<NotificationSettings> notificationSettings)
        {
            _notificationSettings = notificationSettings.Value;
        }

        public virtual void ConstructData(CompanyUserDto _dto, int InactiveDays, string ManagerFirstName)
        {
            string EmailSignature = _notificationSettings.EmailSignature;
            string UserName = _dto.FirstName + " " + _dto.LastName;

            EmailMessage = $"Hi {ManagerFirstName},<br><br>We’re reaching out to inform you that <b>{UserName}</b> has been automatically marked as <b>Dormant</b> following <b>{InactiveDays} days</b> of inactivity.<br><br>" +
                $"The user no longer has access to the system at this time.<br><br>If reactivation is required, you can use the User Management usecase to reactivate the account. " +
                "If you do not have access to this option, please contact the Support team for assistance.<br><br>If access is no longer needed, the Dormant status can remain, or the account may be disabled as appropriate.<br><br>" +
                $"Let us know if you need any support.<br><br>Thank you,<br>{EmailSignature}<br>Support Team<br>(This is a system generated email kindly do not reply)<br>";

            EmailSubject = $"Notification: {UserName} marked as dormant";
        }
    }
}
