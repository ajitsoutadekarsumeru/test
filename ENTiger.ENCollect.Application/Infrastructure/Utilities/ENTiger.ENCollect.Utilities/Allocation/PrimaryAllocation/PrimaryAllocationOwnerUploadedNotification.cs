using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class PrimaryAllocationOwnerUploadedNotification : IMessageTemplate, IFlexUtilityService
    {
        public virtual string EmailSubject { get; set; } = string.Empty;
        public virtual string EmailMessage { get; set; } = string.Empty;
        public virtual string SMSMessage { get; set; } = string.Empty;
        private readonly NotificationSettings _notificationSettings;
        public PrimaryAllocationOwnerUploadedNotification(IOptions<NotificationSettings> notificationSettings)
        {
            _notificationSettings = notificationSettings.Value;
        }
        public virtual void ConstructData(string WorkRequestId)
        {
            string Signature = _notificationSettings.EmailSignature;

            EmailSubject = "Acknowledgement for allocation Owner file receipt -" + WorkRequestId;
            EmailMessage = "Dear User, <br><br>"
                            + "Your file with transaction ID " + WorkRequestId + " has been received. Once uploaded we will send you a confirmation. <br><br>"
                            + "NOTE: this is NOT a success message for UPLOAD.<br>"
                            + "Regards <br>"
                            + Signature + "<br>"
                            + "(This is a system generated email. Please do not reply back)<br>";
        }
    }
}