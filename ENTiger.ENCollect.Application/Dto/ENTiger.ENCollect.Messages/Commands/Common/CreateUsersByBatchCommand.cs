namespace ENTiger.ENCollect.CommonModule
{
    public class CreateUsersByBatchCommand : FlexCommandBridge<CreateUsersByBatchDto, FlexAppContextBridge>
    {
        public string CustomId { get; set; }
    }
}