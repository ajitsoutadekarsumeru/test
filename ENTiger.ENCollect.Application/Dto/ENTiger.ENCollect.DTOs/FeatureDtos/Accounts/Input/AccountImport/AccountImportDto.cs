namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AccountImportDto : DtoBridge
    {
        public string? filename { get; set; }

        public string? customid { get; set; }
    }
}