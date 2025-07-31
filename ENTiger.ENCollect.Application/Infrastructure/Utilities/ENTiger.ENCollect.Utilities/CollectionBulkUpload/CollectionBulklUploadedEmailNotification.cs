using Sumeru.Flex;

namespace ENTiger.ENCollect.Utilities.BulkTrailUpload
{
    public class CollectionBulklUploadedEmailNotification : IMessageTemplate, IFlexUtilityService
    {
        public string EmailSubject { get; set; } = string.Empty;
        public string EmailMessage { get; set; } = string.Empty;
        public string SMSMessage { get; set; } = string.Empty;

        public virtual void ConstructData(CollectionUploadFileDtoWithId _dto)
        {
            EmailMessage = "<p>Dear User, <br><br>" +
                "Your file with TransactionId - " + _dto.CustomId + " has been received. Once uploaded we will send you a confirmation.<br><br>" +
                "FileName : " + _dto.FileName + "<br><br>" +
                "NOTE: This is NOT a success message for UPLOAD.<br></p>";

            EmailSubject = "Collection Bulk Upload - " + _dto.CustomId;
        }
    }
}