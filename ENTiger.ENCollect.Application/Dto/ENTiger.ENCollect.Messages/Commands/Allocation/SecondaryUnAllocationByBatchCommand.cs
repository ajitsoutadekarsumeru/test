namespace ENTiger.ENCollect.AllocationModule
{
    public class SecondaryUnAllocationByBatchCommand : FlexCommandBridge<SecondaryUnAllocationByBatchDto, FlexAppContextBridge>
    {
        public string CustomId { get; set; }
    }
}