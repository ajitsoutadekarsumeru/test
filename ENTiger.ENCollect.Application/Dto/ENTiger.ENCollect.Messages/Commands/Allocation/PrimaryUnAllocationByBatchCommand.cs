namespace ENTiger.ENCollect.AllocationModule
{
    public class PrimaryUnAllocationByBatchCommand : FlexCommandBridge<PrimaryUnAllocationByBatchDto, FlexAppContextBridge>
    {
        public string CustomId { get; set; }
    }
}