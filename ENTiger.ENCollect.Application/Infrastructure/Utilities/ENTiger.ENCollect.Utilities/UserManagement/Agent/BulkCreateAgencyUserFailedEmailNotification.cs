using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect.Utilities.UserManagement.Agent
{
    public class BulkCreateAgencyUserFailedEmailNotification : IMessageTemplate, IFlexUtilityService
    {
        public string EmailSubject { get; set; } = string.Empty;
        public string EmailMessage { get; set; } = string.Empty;
        public string SMSMessage { get; set; } = string.Empty;
        private readonly NotificationSettings _notificationSettings;
        public BulkCreateAgencyUserFailedEmailNotification(IOptions<NotificationSettings> notificationSettings)
        {
            _notificationSettings = notificationSettings.Value;
        }
        public virtual void ConstructData(string TransactionId)
        {
            string Signature = _notificationSettings.EmailSignature;

            EmailSubject = "Bulk Upload of Agent -" + TransactionId;
            EmailMessage = "Dear User, <br><br>"
            + "Your Bulk Upload of Agent file having Transaction Id number " + TransactionId + " has been rejected due to incorrect headers in the file or the file format being incorrect.<br><br>"
            + "Regards <br>"
            + Signature + "<br>"
            + "(This is a system generated email. Please do not reply back)<br>";
        }
    }
}