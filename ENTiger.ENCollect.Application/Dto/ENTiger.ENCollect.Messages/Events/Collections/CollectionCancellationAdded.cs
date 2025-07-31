namespace ENTiger.ENCollect.CollectionsModule
{
    public class CollectionCancellationAdded : FlexEventBridge<FlexAppContextBridge>
    {
        public string Id { get; set; }
        public string MobileNo { get; set; }
    }
}