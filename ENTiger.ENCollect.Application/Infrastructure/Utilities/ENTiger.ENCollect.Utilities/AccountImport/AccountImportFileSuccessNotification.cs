using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class AccountImportFileSuccessNotification : IMessageTemplate, IFlexUtilityService
    {
        public virtual string EmailSubject { get; set; } = string.Empty;
        public virtual string EmailMessage { get; set; } = string.Empty;
        public virtual string SMSMessage { get; set; } = string.Empty;

        public virtual void ConstructData(string TransactionId, string FileName)
        {
            EmailSubject = "Processed : Bulk Account Import via File - " + TransactionId;

            EmailMessage = "<p>Dear Team,<br/><br/>" +
                                "File has been proccessed successfully.<br/><br/>" +
                                "TransactionId  : " + TransactionId + "<br/>" +
                                "FileName       : " + FileName + "<br/><br/>" +
                                "Please contact the administrator for more details.</p>";
        }
    }
}