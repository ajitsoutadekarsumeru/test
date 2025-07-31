namespace ENTiger.ENCollect.CollectionsModule
{
    public class CollectionCancellationRejected : FlexEventBridge<FlexAppContextBridge>
    {
        public string Id { get; set; }
    }
}