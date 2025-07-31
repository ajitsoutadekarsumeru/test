namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchAccountImportFileStatusDto : DtoBridge
    {
        public string TransactionId { get; set; }
        public string FileName { get; set; }
        public string Status { get; set; }
    }
}