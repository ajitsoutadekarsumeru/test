using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class AccountImportFailedNotification : IMessageTemplate, IFlexUtilityService
    {
        public virtual string EmailSubject { get; set; } = string.Empty;
        public virtual string EmailMessage { get; set; } = string.Empty;
        public virtual string SMSMessage { get; set; } = string.Empty;

        public virtual void ConstructData(string Remarks)
        {
            EmailSubject = "Account Import Failed - " + DateTime.Now.ToString("dd-MM-yyyy");

            EmailMessage = "<p>Dear Team,<br/><br/>" +
                                "We apologise for the inconvenience, import of the accounts into system has not been proccessed due to some error.<br/><br/>" +
                                "Reason : " + Remarks + "<br/><br/>" +
                                "Please contact the administrator for more details.</p>";
        }
    }
}