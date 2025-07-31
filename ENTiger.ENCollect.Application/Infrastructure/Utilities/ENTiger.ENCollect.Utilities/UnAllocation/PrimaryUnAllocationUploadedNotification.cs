using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class PrimaryUnAllocationUploadedNotification : IMessageTemplate, IFlexUtilityService
    {
        public virtual string EmailSubject { get; set; } = string.Empty;
        public virtual string EmailMessage { get; set; } = string.Empty;
        public virtual string SMSMessage { get; set; } = string.Empty;

        public virtual void ConstructData(string TransactionId, string FileName, string UploadType)
        {
            EmailSubject = "Bulk Primay UnAllocation Upload - " + TransactionId;

            EmailMessage = "<p>Dear User, <br><br>" +
                                "Your file with TransactionId - " + TransactionId
                                + " has been received. Once uploaded we will send you a confirmation.<br><br>"
                                + "FileType : " + UploadType + "<br>" +
                                "FileName : " + FileName + "<br><br>" +
                                "NOTE: This is NOT a success message for UPLOAD.<br></p>";
        }
    }
}