namespace ENTiger.ENCollect.CommonModule
{
    public class CreateUsersFailed : FlexEventBridge<FlexAppContextBridge>
    {
        public string Id { get; set; }
    }
}