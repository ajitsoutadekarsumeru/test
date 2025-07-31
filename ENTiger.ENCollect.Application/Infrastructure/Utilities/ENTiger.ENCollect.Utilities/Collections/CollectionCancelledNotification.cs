using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class CollectionCancelledNotification : IMessageTemplate, IFlexUtilityService
    {
        public virtual string EmailSubject { get; set; } = string.Empty;
        public virtual string EmailMessage { get; set; } = string.Empty;
        public virtual string SMSMessage { get; set; } = string.Empty;

        private readonly NotificationSettings _notificationSettings;
        public CollectionCancelledNotification(IOptions<NotificationSettings> notificationSettings)
        {
            _notificationSettings = notificationSettings.Value;
        }

        public virtual void ConstructData(CollectionDtoWithId _dto)
        {
            var SMSSignature = _notificationSettings.SmsSignature;

            DateTime date = Convert.ToDateTime(_dto.CollectionDate);
            string receiptIssuanceDate = String.Format("{0:dd-MMM-yyyy}", date);
            SMSMessage = "Cancellation requested for eReceipt Number " + _dto.CustomId
                + ", Received Rs. " + _dto.Amount + " by "
                + _dto.CollectionMode + " vide towards Loan A / c No. "
                + _dto.Account.CustomId + " is CANCELLED. Thanks "
                + SMSSignature + "";

            EmailMessage =
                    "<p>CANCELLATION EMAIL: Receipt No. " + _dto.CustomId + "</p><br/>" +
                    "<p>Dear " + _dto.CustomerName + ",</p><br/>" +
                    "<p>" +
                    "eReceipt Number " + _dto.CustomId + " issued towards payment of Rs. " + _dto.Amount + " by " + _dto.CollectionMode + " under your Loan Account No. "
                    + _dto.Account.CustomId + " is CANCELLED. <br/>" +
                    "<b>Receiving Agent Name:</b> " + _dto.Collector.FirstName + " " + _dto.Collector.LastName + ".<br/>" +
                    "<b>Pan Number (if provided):</b> " + _dto.yPANNo + ". <br/>" +
                    "</p><br/>" +
                    "<p>Thanks & regards <br/>" +
                    "ENCollect.<br/>" +
                    "For any queries contact our customer service helpline number 1860-500-9900<br/>" +
                    "Refer our website for Terms & Conditions</p>";

            EmailSubject = "enCollect : Receipt for Cancellation Request by you";
        }
    }
}