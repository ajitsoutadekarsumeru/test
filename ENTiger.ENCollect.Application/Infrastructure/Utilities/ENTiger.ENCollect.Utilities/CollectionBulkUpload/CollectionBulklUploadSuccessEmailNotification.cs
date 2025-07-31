using Sumeru.Flex;

namespace ENTiger.ENCollect.Utilities.BulkTrailUpload
{
    public class CollectionBulklUploadSuccessEmailNotification : IMessageTemplate, IFlexUtilityService
    {
        public string EmailSubject { get; set; } = string.Empty;
        public string EmailMessage { get; set; } = string.Empty;
        public string SMSMessage { get; set; } = string.Empty;

        public virtual void ConstructData(CollectionUploadFileDtoWithId _dto)
        {
            EmailMessage = "Dear User, <br><br>" +
                                "PFA for the status of records uploaded through batch upload.<br><br>" +
                                "TransactionId : " + _dto.CustomId + "<br><br>" +
                                "(This is a system generated email. Please do not reply back)<br><br>";

            EmailSubject = "Acknowledgement for Collection Bulk Upload - " + _dto.CustomId;
        }
    }
}