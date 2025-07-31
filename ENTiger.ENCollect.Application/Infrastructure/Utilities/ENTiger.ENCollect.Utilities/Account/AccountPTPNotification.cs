using ENTiger.ENCollect.AccountsModule;
using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class AccountPTPNotification : IMessageTemplate, IFlexUtilityService
    {
        public virtual string EmailSubject { get; set; } = string.Empty;
        public virtual string EmailMessage { get; set; } = string.Empty;
        public virtual string SMSMessage { get; set; } = string.Empty;
        private readonly NotificationSettings _notificationSettings;
        public AccountPTPNotification(IOptions<NotificationSettings> notificationSettings)
        {
            _notificationSettings = notificationSettings.Value;
        }
        public virtual void ConstructData(AccountMessageDto dto)
        {
            string SMSSignature = _notificationSettings.SmsSignature;

            SMSMessage = "Dear Customer, We will re-visit you on " + dto.Date +
                        " as you have promised to pay on this date. Please keep the amount handy. Thanks, " + SMSSignature + "";
        }
    }
}