namespace ENTiger.ENCollect.CommonModule
{
    public class UpdateUsersProcessed : FlexEventBridge<FlexAppContextBridge>
    {
        public string Id { get; set; }
    }
}