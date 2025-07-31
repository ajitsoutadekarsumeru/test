namespace ENTiger.ENCollect.FeedbackModule
{
    public class BulkTrailFailedEvent : FlexEventBridge<FlexAppContextBridge>
    {
        public string Id { get; set; }
    }
}