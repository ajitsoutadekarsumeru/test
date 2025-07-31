using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class AccountImportReceivedNotification : IMessageTemplate, IFlexUtilityService
    {
        public virtual string EmailSubject { get; set; } = string.Empty;
        public virtual string EmailMessage { get; set; } = string.Empty;
        public virtual string SMSMessage { get; set; } = string.Empty;

        public virtual void ConstructData(string File, string FileExtension)
        {
            if (!string.IsNullOrEmpty(File))
            {
                EmailSubject = "Bulk Account Import - File Received";

                EmailMessage = "<p>Dear User, <br><br>" +
                        "Your file has been received. Once uploaded we will send you a confirmation.<br><br>" +
                        "FileType : " + FileExtension + " <br>" +
                        "FileName : " + File + "<br><br>" +
                        "NOTE: This is NOT a success message for UPLOAD.</p>";
            }
            else
            {
                EmailSubject = "Bulk Account Import - File Not Found";

                EmailMessage = "<p>Dear User, <br><br>" +
                        "Please place the bulk account import file in the folder.<br><br>" +
                        "FileType : " + FileExtension + " <br><br>" +
                        "Please contact the administrator for more details.</p>";
            }
        }
    }
}