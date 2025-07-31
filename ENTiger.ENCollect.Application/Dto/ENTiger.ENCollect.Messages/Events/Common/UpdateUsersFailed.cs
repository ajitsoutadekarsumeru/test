namespace ENTiger.ENCollect.CommonModule
{
    public class UpdateUsersFailed : FlexEventBridge<FlexAppContextBridge>
    {
        public string Id { get; set; }
    }
}