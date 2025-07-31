namespace ENTiger.ENCollect.CommonModule
{
    public class UpdateUsersByBatchCommand : FlexCommandBridge<UpdateUsersByBatchDto, FlexAppContextBridge>
    {
        public string CustomId { get; set; }
    }
}