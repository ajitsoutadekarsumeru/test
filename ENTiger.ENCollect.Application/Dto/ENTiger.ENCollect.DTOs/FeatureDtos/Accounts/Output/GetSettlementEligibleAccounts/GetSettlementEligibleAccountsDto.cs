namespace ENTiger.ENCollect.AccountsModule
{
    public partial class GetSettlementEligibleAccountsDto : DtoBridge
    {
        public string? AccountId { get; set; }
        public string? AccountNumber { get; set; }
        public string? ProductGroup { get; set; }
        public string? CustomerId { get; set; }        
        public string? CustomerName { get; set; }
        public long? CurrentDPD { get; set; }
        public decimal? TOS { get; set; }
        public string? NPAFlag { get; set; }
        public string? FlaggedAsEligible { get; set; }
        public bool IsSettlementCreated { get; set; }

        public string? MobileNumber { get; set; }
        public int? CurrentBucket { get; set; }
        public decimal? InterestOutStanding { get; set; }
        public decimal? ChargesOutStanding { get; set; }
        public decimal? POS { get; set; }
        public int? NoOfEMIDue { get; set; }
    }
}
