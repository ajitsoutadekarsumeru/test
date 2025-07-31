using Sumeru.Flex;

namespace ENTiger.ENCollect.Utilities.Masters
{
    public class MasterImportNoRecordsEmailNotification : IMessageTemplate, IFlexUtilityService
    {
        public string EmailSubject { get; set; } = string.Empty;
        public string EmailMessage { get; set; } = string.Empty;
        public string SMSMessage { get; set; } = string.Empty;

        public virtual void ConstructData(MasterFileStatusDtoWithId _dto, int recordsinserted, int recordsupdated, int nooferrorrecords, int totalrecords)
        {
            EmailMessage = "<p>Hi,<br/>" +
                       "<p> " + _dto.UploadType + " Import file uploaded but failed to get processed beacuse file contian no data.<br/>" +
                       "<p>" + "FileName: " + _dto.FileName + "<br/>" +
                       "<p>" + "No Of Records          : " + totalrecords + "<br/><br/>" +
                        "<p>" + "No Of Records Inserted : " + recordsinserted + "<br/><br/>" +
                        "<p>" + "No Of Records Updated  : " + recordsupdated + "<br/><br/>" +
                        "<p>" + "No Of Error Records    : " + nooferrorrecords + "<br/><br/>";
            EmailSubject = "Error : Confirmation of " + _dto.UploadType + " Import";
        }
    }
}