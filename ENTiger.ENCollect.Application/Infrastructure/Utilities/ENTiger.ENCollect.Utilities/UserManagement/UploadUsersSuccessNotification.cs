using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class UploadUsersSuccessNotification : IMessageTemplate, IFlexUtilityService
    {
        public virtual string EmailSubject { get; set; } = string.Empty;
        public virtual string EmailMessage { get; set; } = string.Empty;
        public virtual string SMSMessage { get; set; } = string.Empty;

        public virtual void ConstructData(string TransactionId, string FileName)
        {
            EmailSubject = "Bulk Users Update Upload -" + TransactionId;

            EmailMessage = "Dear User, <br><br>" +
                            "PFA for the status of records uploaded through batch upload.<br><br>" +
                            "TransactionId : " + TransactionId + "<br>" +
                            "FileName      : " + FileName + "<br><br>";
        }
    }
}