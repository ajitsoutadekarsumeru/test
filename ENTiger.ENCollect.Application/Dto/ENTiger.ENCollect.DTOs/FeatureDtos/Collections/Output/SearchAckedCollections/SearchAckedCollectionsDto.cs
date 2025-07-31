namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchAckedCollectionsDto : DtoBridge
    {
        public string? ReceiptNo { get; set; }

        public string? CollectorId { get; set; }

        public string? CollectorFistName { get; set; }

        public string? CollectorMiddleName { get; set; }

        public string? CollectorLastName { get; set; }

        public string? CustomerName { get; set; }

        public string? AccountNo { get; set; }

        public string? PaymentMode { get; set; }

        public decimal? Amount { get; set; }

        public DateTime? ReceiptDate { get; set; }

        public DateTime? DateReceivedAtAgency { get; set; }

        public string? Id { get; set; }

        public string? InstrumentNo { get; set; }

        public string? DraweeBank { get; set; }

        public string? DraweeBranch { get; set; }

        public string? OverdueAmount { get; set; }

        public string? ForeclosureAmount { get; set; }

        public string? BounceCharges { get; set; }

        public string? PenalAmount { get; set; }

        public string? TransactionNumber { get; set; }

        public string? OtherCharges { get; set; }

        public string? SettlementAmount { get; set; }

        public string? ProductGroup { get; set; }

        public string? EBCCharge { get; set; }

        public string? CollectionPickupCharge { get; set; }
    }
}