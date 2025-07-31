namespace ENTiger.ENCollect.CommonModule
{
    public class UpdateUsersUploadedEvent : FlexEventBridge<FlexAppContextBridge>
    {
        public string Id { get; set; }
    }
}