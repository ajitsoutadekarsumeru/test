using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class AccountImportFileFailedNotification : IMessageTemplate, IFlexUtilityService
    {
        public virtual string EmailSubject { get; set; } = string.Empty;
        public virtual string EmailMessage { get; set; } = string.Empty;
        public virtual string SMSMessage { get; set; } = string.Empty;

        public virtual void ConstructData(string TransactionId, string Remarks)
        {
            EmailSubject = "Failed : Bulk Account Import via File - " + TransactionId;

            EmailMessage = "<p>Dear Team,<br/><br/>" +
                                "We apologise for the inconvenience, import of the accounts into system has not been proccessed due to some error.<br/><br/>" +
                                "TransactionId  : " + TransactionId + "<br/>" +
                                "Reason         : " + Remarks + "<br/><br/>" +
                                "Please contact the administrator for more details.</p>";
        }
    }
}