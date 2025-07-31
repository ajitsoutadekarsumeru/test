using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect.Utilities.BulkTrailUpload
{
    public class CollectionBulkUploadFailedEmailNotification : IMessageTemplate, IFlexUtilityService
    {
        public string EmailSubject { get; set; } = string.Empty;
        public string EmailMessage { get; set; } = string.Empty;
        public string SMSMessage { get; set; } = string.Empty;

        private readonly NotificationSettings _notificationSettings;
        public CollectionBulkUploadFailedEmailNotification(IOptions<NotificationSettings> notificationSettings)
        {
            _notificationSettings = notificationSettings.Value;
        }

        public virtual void ConstructData(CollectionUploadFileDtoWithId _dto)
        {
            string Signature = _notificationSettings.EmailSignature;
            string msg = string.Empty;
            string subject = "Collection Bulk Upload Rejected - " + _dto.CustomId;
            msg = "Dear User, <br><br>";
            msg += "Your Bulk Collection Upload file having Transaction Id number " + _dto.CustomId +
                    " has been rejected due to incorrect headers in the file or the file format being incorrect.<br><br>";
            msg += "Regards <br>";
            msg += Signature + "<br>";
            msg += "(This is a system generated email. Please do not reply back)<br>";

            EmailMessage = msg;

            EmailSubject = subject;
        }
    }
}