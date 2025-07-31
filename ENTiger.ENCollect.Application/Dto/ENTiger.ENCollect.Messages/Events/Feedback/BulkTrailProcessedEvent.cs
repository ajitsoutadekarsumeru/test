namespace ENTiger.ENCollect.FeedbackModule
{
    public class BulkTrailProcessedEvent : FlexEventBridge<FlexAppContextBridge>
    {
        public string? Id { get; set; }
    }
}