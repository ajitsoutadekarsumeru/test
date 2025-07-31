namespace ENTiger.ENCollect.AllocationModule
{
    public class SecondaryAllocationFailedEvent : FlexEventBridge<FlexAppContextBridge>
    {
        public string? Id { get; set; }
        public string? FileType { get; set; }
    }
}