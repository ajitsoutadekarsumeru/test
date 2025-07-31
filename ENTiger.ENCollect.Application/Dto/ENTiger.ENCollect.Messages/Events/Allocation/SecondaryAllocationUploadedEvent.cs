namespace ENTiger.ENCollect.AllocationModule
{
    public class SecondaryAllocationUploadedEvent : FlexEventBridge<FlexAppContextBridge>
    {
        public string? Id { get; set; }
        public string? CustomId { get; set; }
        public string? ToEmailAddress { get; set; }
        public string? FileType { get; set; }
        public string? FileName { get; set; }
        public string? AllocationMethod { get; set; }
    }
}