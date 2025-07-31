namespace ENTiger.ENCollect.AccountsModule
{
    public class AccountImportProcessedEvent : FlexEventBridge<FlexAppContextBridge>
    {
        public string Id { get; set; }
    }
}