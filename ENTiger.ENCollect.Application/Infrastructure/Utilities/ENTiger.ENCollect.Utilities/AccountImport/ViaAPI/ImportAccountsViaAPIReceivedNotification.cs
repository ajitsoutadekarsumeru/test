using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class ImportAccountsViaAPIReceivedNotification : IMessageTemplate, IFlexUtilityService
    {
        public virtual string EmailSubject { get; set; } = string.Empty;
        public virtual string EmailMessage { get; set; } = string.Empty;
        public virtual string SMSMessage { get; set; } = string.Empty;

        public virtual void ConstructData(string TransactionId)
        {
            EmailSubject = "Received Accounts via API -" + TransactionId;

            EmailMessage = "<p>Dear User, <br/><br/>" +
                                "Your accounts via API with TransactionId - "
                                + TransactionId + " has been received. Once processed we will send you a confirmation.<br/><br/>" +
                                "NOTE: This is NOT a success message for UPLOAD.</p>";
        }
    }
}