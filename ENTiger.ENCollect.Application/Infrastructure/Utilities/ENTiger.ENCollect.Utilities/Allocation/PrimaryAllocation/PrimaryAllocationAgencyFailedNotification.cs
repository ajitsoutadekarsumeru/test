using ENTiger.ENCollect.DomainModels.Settings;
using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class PrimaryAllocationAgencyFailedNotification : IMessageTemplate, IFlexUtilityService
    {
        public virtual string EmailSubject { get; set; } = string.Empty;
        public virtual string EmailMessage { get; set; } = string.Empty;
        public virtual string SMSMessage { get; set; } = string.Empty;

        private readonly NotificationSettings _notificationSettings;
        public PrimaryAllocationAgencyFailedNotification(IOptions<NotificationSettings> notificationSettings)
        {
            _notificationSettings = notificationSettings.Value;            
        }
        public virtual void ConstructData(string WorkRequestId)
        {
            string Signature = _notificationSettings.EmailSignature;

            EmailSubject = "Primary Batch Upload Rejected-" + WorkRequestId;
            EmailMessage = "Dear User, <br><br>"
            + "Your Primary allocation file having Transaction Id number " + WorkRequestId + " has been rejected due to incorrect headers in the file or the file format being incorrect.<br><br>"
            + "Note the correct headers should be : AccountNo & AgencyCode & AllocationExpiryDate without any double quotes or spaces.<br>"
            + "Regards <br>"
            + Signature + "<br>"
            + "(This is a system generated email. Please do not reply back)<br>";
        }
    }
}