namespace ENTiger.ENCollect.AccountsModule
{
    public class AccountImportFailedEvent : FlexEventBridge<FlexAppContextBridge>
    {
        public string Id { get; set; }
        public string Remarks { get; set; }
        public string FileName { get; set; }
    }
}