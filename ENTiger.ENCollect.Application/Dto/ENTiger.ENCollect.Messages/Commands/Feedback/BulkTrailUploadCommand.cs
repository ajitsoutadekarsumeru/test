namespace ENTiger.ENCollect.FeedbackModule
{
    public class BulkTrailUploadCommand : FlexCommandBridge<BulkTrailUploadDto, FlexAppContextBridge>
    {
        public string? CustomId { get; set; }
    }
}