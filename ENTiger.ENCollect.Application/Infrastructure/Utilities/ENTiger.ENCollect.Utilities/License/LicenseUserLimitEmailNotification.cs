using ENTiger.ENCollect.ApplicationUsersModule;
using Microsoft.Extensions.Options;
using Sumeru.Flex;
using System.Text;

namespace ENTiger.ENCollect.Utilities.License
{
    public class LicenseUserLimitEmailNotification : IMessageTemplate, IFlexUtilityService
    {
        public string EmailSubject { get; set; } = string.Empty;
        public string EmailMessage { get; set; } = string.Empty;
        public string SMSMessage { get; set; } = string.Empty;
        private readonly NotificationSettings _notificationSettings;

        public LicenseUserLimitEmailNotification(IOptions<NotificationSettings> notificationSettings)
        {
            _notificationSettings = notificationSettings.Value;
        }

        public virtual void ConstructData(SendLicenseUserLimitMassageDto _dto)
        {
            string EmailSignature = _notificationSettings.EmailSignature;

            // Construct email subject and message 
            EmailSubject = $"ENCollect Pro License Limits";
            StringBuilder sb = new StringBuilder();
            sb.Append($"Dear {_dto.UserName},<br><br>This is to inform you that your license allocation for ENCollect Pro under the user type {_dto.UserType} is nearing its maximum limit.<br><br>");
            sb.Append("Please contact our Sales Team at your earliest convenience to request an increase in your license quota. <br><br>");
            sb.Append($"Kindly note that once the current limit is reached, you will no longer be able to create new users under the {_dto.UserType} user type.<br><br>");
            sb.Append("<table style='border: 1px solid black; border-collapse:collapse; padding: 4px;'>");
            sb.Append("<tr><th><b>User Type</b></th><th><b>Current Limit</b></th><th><b>Count of approved users</b></th></tr>");
            foreach (var item in _dto.UserTypeDetails)
            {
                sb.Append($"<tr><td>{item.UserType}</td><td>{item.Limit}</td><td>{item.CurrentConsumption}</td></tr>");
            }
            sb.Append("</table><br><br>Regards,<br>ENCollect Pro");
            EmailMessage = sb.ToString();
        }
    }
}