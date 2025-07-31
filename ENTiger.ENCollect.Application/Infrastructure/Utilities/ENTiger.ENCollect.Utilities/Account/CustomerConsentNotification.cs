using ENTiger.ENCollect.AccountsModule;
using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class CustomerConsentNotification : IMessageTemplate, IFlexUtilityService
    {
        public virtual string EmailSubject { get; set; } = string.Empty;
        public virtual string EmailMessage { get; set; } = string.Empty;
        public virtual string SMSMessage { get; set; } = string.Empty;
        private readonly NotificationSettings _notificationSettings;
        public CustomerConsentNotification(IOptions<NotificationSettings> notificationSettings)
        {
            _notificationSettings = notificationSettings.Value;
        }
        public virtual void ConstructData(CustomerConsentMessageDto dto)
        {
            string SMSSignature = _notificationSettings.SmsSignature;
            string EmailSignature = _notificationSettings.EmailSignature;

            SMSMessage = $"Dear Customer, We hope you are well, we'd like to confirm a visit with our collection agent on {string.Format("{0:dd/MM/yyyy}", dto.Date)}, at {string.Format("{0:hh:mm tt}", dto.Date)}. " +
                $"We understand this is after banking hours and want to ensure its convenient for you. Kindly use this link {dto.Link} to submit your response. {dto.ClientName ?? ""} {SMSSignature}";
            // Style the anchor tag if already present
            string hyperlink = $"<a href='{dto.Link}' style='color:blue; text-decoration:underline;'>Link</a>";

            // Construct email subject and message (without closing line)
            EmailSubject = $"{dto.ClientName ?? ""} Customer Consent";
            EmailMessage = $@"
                              Dear Customer.<br><br>
                              Greetings for the Day!<br><br>
                             
                              We hope you are well, we’d like to confirm visit with our collection agent on {dto.Date:dd/MM/yyyy}, at {dto.Date:hh:mm tt}.<br>
                              We understand this is after banking hours and want to ensure its convenient for you.<br>
                              Kindly use below link to submit your response.<br>
                              {hyperlink}<br><br>";
                             
        }
    }
}
