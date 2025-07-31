using Sumeru.Flex;

namespace ENTiger.ENCollect.Utilities.UserManagement.Agency
{
    public class BulkCreateAgencySuccessEmailNotification : IMessageTemplate, IFlexUtilityService
    {
        public string EmailSubject { get; set; } = string.Empty;
        public string EmailMessage { get; set; } = string.Empty;
        public string SMSMessage { get; set; } = string.Empty;

        public virtual void ConstructData(string TransactionId, string FileName)
        {
            EmailSubject = "Bulk Agency Upload - " + TransactionId;

            EmailMessage = "Dear User, <br><br>" +
                                "PFA for the status of records uploaded through batch upload.<br><br>" +
                                "TransactionId : " + TransactionId + "<br>" +
                                "FileName      : " + FileName + "<br><br>";
        }
    }
}