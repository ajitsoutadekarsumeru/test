using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect.Utilities.UserManagement.Agent
{
    public class AgencyUserCreatedSMSNotification : IMessageTemplate, IFlexUtilityService
    {
        public string EmailSubject { get; set; } = string.Empty;
        public string EmailMessage { get; set; } = string.Empty;
        public string SMSMessage { get; set; } = string.Empty;
        private readonly NotificationSettings _notificationSettings;

        public AgencyUserCreatedSMSNotification(IOptions<NotificationSettings> notificationSettings)
        {
            _notificationSettings = notificationSettings.Value;
        }

        public virtual void ConstructData(AgencyUserDtoWithId _dto)
        {
            string SMSSignature = _notificationSettings.SmsSignature;
            SMSMessage = "Your profile has been created with UserName " + _dto.PrimaryEMail + ". Kindly await approval for the same. " + SMSSignature + "";
        }
    }
}