namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetTodaysPTPAccountsDto : DtoBridge
    {
        public string? ID { get; set; }
        public DateTime? PTP { get; set; }
        public string? AccountNo { get; set; }
        public string? FirstName { get; set; }
        public string? Bucket { get; set; }
        public string? POS { get; set; }
        public string? Area { get; set; }
        public string? Lattitude { get; set; }
        public string? Longitude { get; set; }
        public string? ProductCode { get; set; }
        public string? CreditCardNo { get; set; }
        public decimal? PTPAmount { get; set; }
        public string? CustomerID { get; set; }
        public decimal? TotalOverDueAmount { get; set; }
    }
}