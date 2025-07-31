using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class AccountImportFileReceivedNotification : IMessageTemplate, IFlexUtilityService
    {
        public virtual string EmailSubject { get; set; } = string.Empty;
        public virtual string EmailMessage { get; set; } = string.Empty;
        public virtual string SMSMessage { get; set; } = string.Empty;

        public virtual void ConstructData(string TransactionId, string FileName)
        {
            EmailSubject = "Received : Bulk Account Import via File - " + TransactionId;

            EmailMessage = "<p>Dear User, <br/><br/>" +
                                "Your file has been received. Once uploaded we will send you a confirmation.<br/><br/>" +
                                "TransactionId  : " + TransactionId + "<br/>" +
                                "FileName       : " + FileName + "<br/><br/>" +
                                "Note: This is not a success message for Upload.</p>";
        }
    }
}