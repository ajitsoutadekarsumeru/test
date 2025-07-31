using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class UploadUsersFailedNotification : IMessageTemplate, IFlexUtilityService
    {
        public virtual string EmailSubject { get; set; } = string.Empty;
        public virtual string EmailMessage { get; set; } = string.Empty;
        public virtual string SMSMessage { get; set; } = string.Empty;

        public virtual void ConstructData(string TransactionId)
        {
            EmailSubject = "Bulk Users Update Upload Rejected - " + TransactionId;

            EmailMessage = "Dear User, <br><br>" +
                            "Your users update file having TransactionId - "
                            + TransactionId + " has been rejected due to incorrect headers in the file or the file format being incorrect.<br><br>";
            //"Note : Correct headers should be : AccountNo without any double quotes or spaces.<br>";
        }
    }
}