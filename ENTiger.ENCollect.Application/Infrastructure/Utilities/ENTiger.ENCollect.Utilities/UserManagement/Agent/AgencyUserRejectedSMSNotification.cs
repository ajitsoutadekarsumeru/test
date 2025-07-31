using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect.Utilities.UserManagement.Agent
{
    public class AgencyUserRejectedSMSNotification : IMessageTemplate, IFlexUtilityService
    {
        public string EmailSubject { get; set; } = string.Empty;
        public string EmailMessage { get; set; } = string.Empty;
        public string SMSMessage { get; set; } = string.Empty;
        private readonly NotificationSettings _notificationSettings;

        public AgencyUserRejectedSMSNotification(IOptions<NotificationSettings> notificationSettings)
        {
            _notificationSettings = notificationSettings.Value;
        }

        public virtual void ConstructData(AgencyUserDtoWithId _dto)
        {
            string SMSSignature = _notificationSettings.SmsSignature;
            SMSMessage = "Request for profile creation for your ID has been rejected. Contact your supervisor for details. " + SMSSignature + "";
        }
    }
}