using ENTiger.ENCollect.AccountsModule;
using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class CustomerConsentResponseNotification : IMessageTemplate, IFlexUtilityService
    {
        public virtual string EmailSubject { get; set; } = string.Empty;
        public virtual string EmailMessage { get; set; } = string.Empty;
        public virtual string SMSMessage { get; set; } = string.Empty;
        private readonly NotificationSettings _notificationSettings;
        public CustomerConsentResponseNotification(IOptions<NotificationSettings> notificationSettings)
        {
            _notificationSettings = notificationSettings.Value;
        }

        public virtual void ConstructData(CustomerConsentResponseMessageDto dto)
        {
            string SMSSignature = _notificationSettings.SmsSignature;
            string EmailSignature = _notificationSettings.EmailSignature;

            SMSMessage = $"Visit request for {dto.ClientName ?? ""} ({dto.AccountNo?.Substring(0, dto.AccountNo.Length > 4 ? dto.AccountNo.Length - 4 : dto.AccountNo.Length) ?? ""}XXXX) on " +
            $"{string.Format("{0:dd/MM/yyyy}", dto.Date)} at {string.Format("{0:hh:mm tt}", dto.Date)} has been {dto.Status ?? ""}. {SMSSignature}";

            EmailSubject = $"Account {new string('X', Math.Max(0, dto.AccountNo?.Length - 4 ?? 0))}{dto.AccountNo?.Substring(Math.Max(0, dto.AccountNo.Length - 4))} Customer Consent Request";
            EmailMessage = $"Dear {dto.UserName ?? "User"}," +
            $"<br><br>Your visit request has been {dto.Status ?? ""} for:<br><br>" +
            $"Customer Name: {dto.ClientName ?? ""}<br>" +
            $"Account Number: {new string('X', Math.Max(0, dto.AccountNo?.Length - 4 ?? 0))}{dto.AccountNo?.Substring(Math.Max(0, dto.AccountNo.Length - 4))}<br>" +
            $"Date: {string.Format("{0:dd/MM/yyyy}", dto.Date)}<br>Time: {string.Format("{0:hh:mm tt}", dto.Date)}<br>" +
            $"<br><br>Thank you for using ENCollect.<br><br>Best regards,<br>{EmailSignature}<br>(This is a system generated email kindly do not reply)<br>";
        }
    }
}
