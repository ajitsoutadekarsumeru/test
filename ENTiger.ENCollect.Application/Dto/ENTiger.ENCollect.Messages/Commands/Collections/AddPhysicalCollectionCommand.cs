namespace ENTiger.ENCollect.CollectionsModule
{
    public class AddPhysicalCollectionCommand : FlexCommandBridge<AddPhysicalCollectionDto, FlexAppContextBridge>
    {
        public string CustomId { get; set; }
    }
}