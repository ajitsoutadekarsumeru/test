namespace ENTiger.ENCollect.AllocationModule
{
    public class PrimaryUnAllocationProcessedEvent : FlexEventBridge<FlexAppContextBridge>
    {
        public string? CustomId { get; set; }
        public string? EMail { get; set; }
        public string? FileName { get; set; }
        public string UnAllocationType { get; set; }
    }    
}
