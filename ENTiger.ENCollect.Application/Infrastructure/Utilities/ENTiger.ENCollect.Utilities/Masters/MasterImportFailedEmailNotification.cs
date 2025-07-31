using Sumeru.Flex;

namespace ENTiger.ENCollect.Utilities.Masters
{
    public class MasterImportFailedEmailNotification : IMessageTemplate, IFlexUtilityService
    {
        public string EmailSubject { get; set; } = string.Empty;
        public string EmailMessage { get; set; } = string.Empty;
        public string SMSMessage { get; set; } = string.Empty;

        public virtual void ConstructData(MasterFileStatusDtoWithId _dto)
        {
            EmailMessage = "<p>Hi,<br/>" +
                       "<p> " + _dto.UploadType + " Import file uploaded but failed to get processed.<br/>" +
                       "<p>" + "FileName: " + _dto.FileName + "<br/>" +
                       "<p>Please Contact the administrator.<br/><br/>";

            EmailSubject = "Failed : Confirmation of " + _dto.UploadType + " Import";
        }
    }
}