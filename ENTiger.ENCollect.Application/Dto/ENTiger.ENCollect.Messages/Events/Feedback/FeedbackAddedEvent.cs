namespace ENTiger.ENCollect.FeedbackModule
{
    public class FeedbackAddedEvent : FlexEventBridge<FlexAppContextBridge>
    {
        public string Id { get; set; }
    }
}