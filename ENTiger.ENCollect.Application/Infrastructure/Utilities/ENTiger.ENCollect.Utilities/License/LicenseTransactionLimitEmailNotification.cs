using ENTiger.ENCollect.ApplicationUsersModule;
using Microsoft.Extensions.Options;
using Sumeru.Flex;
using System.Text;

namespace ENTiger.ENCollect.Utilities.License
{
    public class LicenseTransactionLimitEmailNotification : IMessageTemplate, IFlexUtilityService
    {
        public string EmailSubject { get; set; } = string.Empty;
        public string EmailMessage { get; set; } = string.Empty;
        public string SMSMessage { get; set; } = string.Empty;
        private readonly NotificationSettings _notificationSettings;

        public LicenseTransactionLimitEmailNotification(IOptions<NotificationSettings> notificationSettings)
        {
            _notificationSettings = notificationSettings.Value;
        }

        public virtual void ConstructData(SendLicenseTransactionLimitMassageDto _dto)
        {

            SMSMessage = $"Dear User, Unable to submit transaction. The maximum allowed number of {_dto.TransactionType} ({_dto.Limit} for the month has been reached. Please contact your administrator to increase the limit. ENCollect Pro";         

            // Construct email subject and message
            EmailSubject = $"ENCollect Pro License Limits";
            StringBuilder sb = new StringBuilder();
            sb.Append($"Dear {_dto.UserName},<br><br>Unable to submit transaction. The maximum allowed number of {_dto.TransactionType} ({_dto.Limit} for the month has been reached). Please contact your administrator to increase the limit<br><br>");
            sb.Append("Please contact our Sales Team at your earliest convenience to request an increase in your license quota. <br><br>");
            sb.Append($"Kindly note that once the current limit is reached, you will no longer be able to create new users under the {_dto.TransactionType} transaction.<br><br>");
            sb.Append("<table style='border: 1px solid black; border-collapse:collapse; padding: 4px;'>");
            sb.Append("<tr><th><b>Transaction Type</b></th><th><b>Current Limit</b></th><th><b>Current count of transactions</b></th></tr>");
            foreach (var item in _dto.TransactionTypeDetails)
            {
                sb.Append($"<tr><td>{item.TransactionType}</td><td>{item.Limit}</td><td>{item.CurrentConsumption}</td></tr>");
            }
            sb.Append("</table><br><br>Regards,<br>ENCollect Pro");
            EmailMessage = sb.ToString();
        }
    }
}
