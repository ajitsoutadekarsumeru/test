using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class AccountImportFileErrorNotification : IMessageTemplate, IFlexUtilityService
    {
        public virtual string EmailSubject { get; set; } = string.Empty;
        public virtual string EmailMessage { get; set; } = string.Empty;
        public virtual string SMSMessage { get; set; } = string.Empty;

        public virtual void ConstructData(string TransactionId)
        {
            EmailSubject = "Error : Bulk Account Import via File - " + TransactionId;

            EmailMessage = "<p>Dear Team,<br/><br/>" +
                                "Please find the attached for data errors in upload file.<br/><br/>" +
                                "Please contact the administrator for more details.</p>";
        }
    }
}