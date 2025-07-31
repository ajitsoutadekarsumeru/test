namespace ENTiger.ENCollect.AllocationModule
{
    public class PrimaryAllocationFailedEvent : FlexEventBridge<FlexAppContextBridge>
    {
        public string? Id { get; set; }
        public string? FileType { get; set; }
    }
}