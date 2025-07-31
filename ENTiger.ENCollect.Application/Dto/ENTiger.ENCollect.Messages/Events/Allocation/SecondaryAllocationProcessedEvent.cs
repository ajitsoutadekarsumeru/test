namespace ENTiger.ENCollect.AllocationModule
{
    public class SecondaryAllocationProcessedEvent : FlexEventBridge<FlexAppContextBridge>
    {
        public string? Id { get; set; }
        public string? FileType { get; set; }
    }
}