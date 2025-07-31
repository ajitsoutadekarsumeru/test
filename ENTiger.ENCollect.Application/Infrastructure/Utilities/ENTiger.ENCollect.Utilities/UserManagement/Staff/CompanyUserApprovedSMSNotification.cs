using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect.Utilities.UserManagement.Staff
{
    public class CompanyUserApprovedSMSNotification : IMessageTemplate, IFlexUtilityService
    {
        public string EmailSubject { get; set; } = string.Empty;
        public string EmailMessage { get; set; } = string.Empty;
        public string SMSMessage { get; set; } = string.Empty;
        private readonly NotificationSettings _notificationSettings;

        public CompanyUserApprovedSMSNotification(IOptions<NotificationSettings> notificationSettings)
        {
            _notificationSettings = notificationSettings.Value;
        }

        public virtual void ConstructData(CompanyUserDtoWithId _dto, string tinyUrl)
        {
            string SMSSignature = _notificationSettings.SmsSignature;
            SMSMessage = "Your id has been approved by " + SMSSignature + " team. Your profile User Name " + _dto.PrimaryEMail + " To create your password CLICK HERE " + tinyUrl + " " + SMSSignature + "";
        }
    }
}