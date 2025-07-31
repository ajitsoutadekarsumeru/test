namespace ENTiger.ENCollect.AccountsModule
{
    public class AccountImportCommand : FlexCommandBridge<AccountImportDto, FlexAppContextBridge>
    {
        public string CustomId { get; set; }
    }
}