namespace ENTiger.ENCollect.CommonModule
{
    public class CreateUsersUploadedEvent : FlexEventBridge<FlexAppContextBridge>
    {
        public string Id { get; set; }
    }
}