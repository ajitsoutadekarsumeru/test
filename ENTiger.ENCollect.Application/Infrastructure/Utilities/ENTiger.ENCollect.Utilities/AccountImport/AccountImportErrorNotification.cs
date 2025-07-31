using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class AccountImportErrorNotification : IMessageTemplate, IFlexUtilityService
    {
        public virtual string EmailSubject { get; set; } = string.Empty;
        public virtual string EmailMessage { get; set; } = string.Empty;
        public virtual string SMSMessage { get; set; } = string.Empty;

        public virtual void ConstructData()
        {
            EmailSubject = "Account Import Errors - " + DateTime.Now.ToString("dd-MM-yyyy");

            EmailMessage = "<p>Dear Team,<br/><br/>" +
                            "Please find the attached for data errors in upload file.<br/><br/>" +
                            "Please contact the administrator for more details.</p>";
        }
    }
}