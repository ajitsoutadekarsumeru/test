namespace ENTiger.ENCollect.CollectionsModule;
public class CollectionSuccessEvent : FlexEventBridge<FlexAppContextBridge>
{
    public string CollectionId { get; set; }
}