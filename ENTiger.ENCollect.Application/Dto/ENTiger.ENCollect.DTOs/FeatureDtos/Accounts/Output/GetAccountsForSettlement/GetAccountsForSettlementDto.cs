namespace ENTiger.ENCollect.AccountsModule
{    
    public partial class GetAccountsForSettlementDto : DtoBridge
    {
        public string? AccountId { get; set; }
        public string? AccountNumber { get; set; }
        public string? CustomerId { get; set; }
        public string? ProductGroup { get; set; }
        public string? CustomerName { get; set; }
        public long? CurrentDPD { get; set; }
        public string? NPAFlag { get; set; }
        public decimal? TOS { get; set; }
        public decimal? POS { get; set; }
        public decimal? TotalOverDue { get; set; }
        public bool IsEligibleForSettlement { get; set; }
    }
}
