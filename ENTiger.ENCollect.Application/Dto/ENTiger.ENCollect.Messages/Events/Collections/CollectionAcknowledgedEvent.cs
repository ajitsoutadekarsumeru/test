namespace ENTiger.ENCollect.CollectionsModule
{
    public class CollectionAcknowledgedEvent : FlexEventBridge<FlexAppContextBridge>
    {
        public List<string>? CollectionIds { get; set; }
    }
}