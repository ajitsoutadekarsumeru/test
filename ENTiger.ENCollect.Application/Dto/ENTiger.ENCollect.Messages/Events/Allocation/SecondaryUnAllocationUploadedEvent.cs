namespace ENTiger.ENCollect.AllocationModule
{
    public class SecondaryUnAllocationUploadedEvent : FlexEventBridge<FlexAppContextBridge>
    {
        public string Id { get; set; }
        public string UnAllocationType { get; set; }
    }    
}
 