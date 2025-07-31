using Sumeru.Flex;

namespace ENTiger.ENCollect.Utilities.Masters
{
    public class MasterImportProcessedEmailNotification : IMessageTemplate, IFlexUtilityService
    {
        public string EmailSubject { get; set; } = string.Empty;
        public string EmailMessage { get; set; } = string.Empty;
        public string SMSMessage { get; set; } = string.Empty;

        public virtual void ConstructData(MasterFileStatusDtoWithId _dto, int processedRecords)
        {
            EmailMessage = "<p>Hi,<br/>" +
                       "<p> " + _dto.UploadType + " Import file uploaded and processed successfully.<br/>" +
                       "<p>" + "FileName: " + _dto.FileName + "<br/>" +
                       "<p>" + "No Of Row Count: " + processedRecords + "<br/><br/>";

            EmailSubject = "Success : Confirmation of " + _dto.UploadType + " Import";
        }
    }
}