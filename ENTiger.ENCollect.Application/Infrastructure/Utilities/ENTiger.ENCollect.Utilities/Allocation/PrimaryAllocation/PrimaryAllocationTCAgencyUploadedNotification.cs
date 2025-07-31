using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class PrimaryAllocationTCAgencyUploadedNotification : IMessageTemplate, IFlexUtilityService
    {
        public virtual string EmailSubject { get; set; } = string.Empty;
        public virtual string EmailMessage { get; set; } = string.Empty;
        public virtual string SMSMessage { get; set; } = string.Empty;

        private readonly NotificationSettings _notificationSettings;
        public PrimaryAllocationTCAgencyUploadedNotification(IOptions<NotificationSettings> notificationSettings)
        {
            _notificationSettings = notificationSettings.Value;
        }
        public virtual void ConstructData(string WorkRequestId)
        {
            string Signature = _notificationSettings.EmailSignature;

            EmailSubject = "Acknowledgement for Primary allocation batch file receipt - " + WorkRequestId;
            EmailMessage = "Dear User, <br><br>"
                                + "Your file with transaction ID " + WorkRequestId + " has been received. Once uploaded we will send you a confirmation. <br><br>"
                                + "NOTE: This is NOT a success message for UPLOAD.<br>"
                                + "Regards <br>"
                                + Signature + "<br>"
                                + "(This is a system generated email. Please do not reply back)<br>";
        }
    }
}