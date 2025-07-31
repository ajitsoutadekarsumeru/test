using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect.Utilities.UserManagement
{
    public class AgencyCreatedEmailNotification : IMessageTemplate, IFlexUtilityService
    {
        public string EmailSubject { get; set; } = string.Empty;
        public string EmailMessage { get; set; } = string.Empty;
        public string SMSMessage { get; set; } = string.Empty;
        private readonly NotificationSettings _notificationSettings;

        public AgencyCreatedEmailNotification(IOptions<NotificationSettings> notificationSettings)
        {
            _notificationSettings = notificationSettings.Value;
        }

        public virtual void ConstructData(AgencyDtoWithId _dto)
        {

            string Signature = _notificationSettings.SmsSignature;
            string agencyName = _dto.FirstName;
            EmailMessage = agencyName + " has been submitted for approval. Kindly await further intimation.";

            EmailSubject = "Collection Agency - " + agencyName + " Created.";
        }
    }
}