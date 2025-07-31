using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class ImportAccountsViaAPIFailedNotification : IMessageTemplate, IFlexUtilityService
    {
        public virtual string EmailSubject { get; set; } = string.Empty;
        public virtual string EmailMessage { get; set; } = string.Empty;
        public virtual string SMSMessage { get; set; } = string.Empty;

        public virtual void ConstructData(string TransactionId, string Remarks)
        {
            EmailSubject = "Failed : Import Accounts via API - " + TransactionId;

            EmailMessage = "<p>Dear User, <br/><br/>" +
                                   "Import Accounts via API received but failed to get processed.<br/><br/>" +
                                   "TransactionId  : " + TransactionId + "<br/>" +
                                   "Reason         : " + Remarks + "<br/><br/>" +
                                   "Please contact the administrator for more details.</p>";
        }
    }
}