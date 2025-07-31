namespace ENTiger.ENCollect.FeedbackModule
{
    public class BulkTrailUploadedEvent : FlexEventBridge<FlexAppContextBridge>
    {
        public string? CustomId { get; set; }
        public string? Id { get; set; }
        public string? ToEmailAddress { get; set; }

        public string? FileType { get; set; }
        public string? FileName { get; set; }
    }
}