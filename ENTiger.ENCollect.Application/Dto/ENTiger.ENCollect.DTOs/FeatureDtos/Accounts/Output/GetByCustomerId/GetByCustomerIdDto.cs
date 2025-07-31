namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetByCustomerIdDto : DtoBridge
    {
        public string? Id { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerID { get; set; }
        public string? AccountNo { get; set; }
        public string? TotalOverdueAmount { get; set; }
        public string? ProductGroup { get; set; }
        public long? Current_DPD { get; set; }
        public string? CurrentBucket { get; set; }
        public string? Product { get; set; }
        public decimal? CurrentPOS { get; set; }
    }
}