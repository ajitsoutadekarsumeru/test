namespace ENTiger.ENCollect.AllocationModule
{
    public class PrimaryUnAllocationUploadedEvent : FlexEventBridge<FlexAppContextBridge>
    {
        public string Id { get; set; }
        public string UnAllocationType { get; set; }
    }    
}
 