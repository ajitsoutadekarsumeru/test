using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect.Utilities.UserManagement.Agent
{
    public class AgencyUserApprovedSMSNotification : IMessageTemplate, IFlexUtilityService
    {
        public string EmailSubject { get; set; } = string.Empty;
        public string EmailMessage { get; set; } = string.Empty;
        public string SMSMessage { get; set; } = string.Empty;
        private readonly NotificationSettings _notificationSettings;

        public AgencyUserApprovedSMSNotification(IOptions<NotificationSettings> notificationSettings)
        {
            _notificationSettings = notificationSettings.Value;
        }

        public virtual void ConstructData(AgencyUserDtoWithId _dto, string tinyurl)
        {
            string SMSSignature = _notificationSettings.SmsSignature;
            SMSMessage = "Your id has been approved by " + SMSSignature + " team. Your profile User Name " + _dto.PrimaryEMail + " To create your password CLICK HERE " + tinyurl + " " + SMSSignature + "";
        }
    }
}