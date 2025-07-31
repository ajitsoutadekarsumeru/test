namespace ENTiger.ENCollect.AccountsModule
{
    public class AccountImportUploadedEvent : FlexEventBridge<FlexAppContextBridge>
    {
        public string? Id { get; set; }
    }
}