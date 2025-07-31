using Sumeru.Flex;

namespace ENTiger.ENCollect.Utilities.Masters
{
    public class MasterImportHeaderMismatchEmailNotification : IMessageTemplate, IFlexUtilityService
    {
        public string EmailSubject { get; set; } = string.Empty;
        public string EmailMessage { get; set; } = string.Empty;
        public string SMSMessage { get; set; } = string.Empty;

        public virtual void ConstructData(MasterFileStatusDtoWithId _dto)
        {
            EmailMessage = "<p>Hi,<br/>" +
                                   "<p> " + _dto.UploadType + " Import file uploaded and but not processed successfully because header Mismatched .<br/>" +
                                   "<p>" + "FileName: " + _dto.FileName + "<br/><br/>";

            EmailSubject = "Error : Confirmation of " + _dto.UploadType + " Import";
        }
    }
}