namespace ENTiger.ENCollect.AllocationModule
{
    public class PrimaryAllocationProcessedEvent : FlexEventBridge<FlexAppContextBridge>
    {
        public string? Id { get; set; }
        public string? CustomId { get; set; }
        public string? ToEmailAddress { get; set; }
        public string? FileType { get; set; }
    }
}