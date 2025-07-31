using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class IssueReceiptNotificationSMS : IMessageTemplate, IFlexUtilityService
    {
        public virtual string EmailSubject { get; set; } = string.Empty;
        public virtual string EmailMessage { get; set; } = string.Empty;
        public virtual string SMSMessage { get; set; } = string.Empty;
        private readonly NotificationSettings _notificationSettings;

        public IssueReceiptNotificationSMS(IOptions<NotificationSettings> notificationSettings)
        {
            _notificationSettings = notificationSettings.Value;
        }

        public virtual void ConstructData(CollectionDtoWithId _dto)
        {
            var SMSSignature = _notificationSettings.SmsSignature;

            SMSMessage = "Received Rs. " + _dto.Amount + " by " + _dto.Collector.FirstName
                + " vide eReceipt Number " + _dto.CustomId + " towards your Loan A/C no. "
                + _dto.Account.CustomId + ". Thanks " + SMSSignature + "";
        }
    }
}