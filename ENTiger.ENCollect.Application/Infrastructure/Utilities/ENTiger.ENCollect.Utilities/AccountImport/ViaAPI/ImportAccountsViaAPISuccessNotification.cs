using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class ImportAccountsViaAPISuccessNotification : IMessageTemplate, IFlexUtilityService
    {
        public virtual string EmailSubject { get; set; } = string.Empty;
        public virtual string EmailMessage { get; set; } = string.Empty;
        public virtual string SMSMessage { get; set; } = string.Empty;

        public virtual void ConstructData(string TransactionId)
        {
            EmailSubject = "Processed : Import Accounts via API - " + TransactionId;

            EmailMessage = "<p>Dear User, <br/><br/>" +
                                 "Import Accounts via API received and processed successfully.<br/><br/>" +
                                 "TransactionId  : " + TransactionId + "<br/><br/>" +
                                 "Please contact the administrator for more details.</p>";
        }
    }
}