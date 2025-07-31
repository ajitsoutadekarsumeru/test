namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchMyDepositSlipsDto : DtoBridge
    {
        public string Id { get; set; }
        public string? ENCslipNO { get; set; }
        public DateTime? Depositedate { get; set; }
        public string? Amount { get; set; }
        public string? ModeofPayment { get; set; }
        public string? BankName { get; set; }
        public string? BranchName { get; set; }
        public string? AccountNumber { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
    }
}