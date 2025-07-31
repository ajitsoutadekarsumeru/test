namespace ENTiger.ENCollect.PublicModule
{
    public class ImportAccountsUploadedEvent : FlexEventBridge<FlexAppContextBridge>
    {
        public string? Id { get; set; }
        public string? CustomId { get; set; }
    }
}