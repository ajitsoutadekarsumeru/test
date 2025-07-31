namespace ENTiger.ENCollect.AllocationModule
{
    public class PrimaryUnAllocationFailedEvent : FlexEventBridge<FlexAppContextBridge>
    {
        public string? CustomId { get; set; }
        public string? Email { get; set; }
        public string? UnAllocationType { get; set; }
    }    
}
