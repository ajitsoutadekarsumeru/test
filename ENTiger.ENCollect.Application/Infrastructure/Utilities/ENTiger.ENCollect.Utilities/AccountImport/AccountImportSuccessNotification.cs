using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class AccountImportSuccessNotification : IMessageTemplate, IFlexUtilityService
    {
        public virtual string EmailSubject { get; set; } = string.Empty;
        public virtual string EmailMessage { get; set; } = string.Empty;
        public virtual string SMSMessage { get; set; } = string.Empty;

        public virtual void ConstructData(string FileName)
        {
            EmailSubject = "Account Import Success - " + DateTime.Now.ToString("dd-MM-yyyy");

            EmailMessage = "<p>Dear Team,<br/><br/>" +
                                "File has been proccessed successfully.<br/><br/>" +
                                "FileName       : " + FileName + "<br/>" +
                                "Please contact the administrator for more details.</p>";
        }
    }
}