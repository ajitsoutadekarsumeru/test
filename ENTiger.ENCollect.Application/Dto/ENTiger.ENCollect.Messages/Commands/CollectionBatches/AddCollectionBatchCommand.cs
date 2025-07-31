namespace ENTiger.ENCollect.CollectionBatchesModule
{
    public class AddCollectionBatchCommand : FlexCommandBridge<AddCollectionBatchDto, FlexAppContextBridge>
    {
        public string CustomId { get; set; }
    }
}