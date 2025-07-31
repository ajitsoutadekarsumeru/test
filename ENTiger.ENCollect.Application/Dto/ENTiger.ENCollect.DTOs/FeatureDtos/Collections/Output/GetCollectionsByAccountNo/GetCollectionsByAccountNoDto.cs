namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetCollectionsByAccountNoDto : DtoBridge
    {
        public decimal? Amount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? CollectorName { get; set; }
        public string? CollectionMode { get; set; }
        public string? InstrumentNo { get; set; }
        public string? MICRCode { get; set; }
        public string? AccountNumber { get; set; }
        public string? IFSCCode { get; set; }
        public string? BankName { get; set; }
        public string? BranchName { get; set; }
        public string? BranchCity { get; set; }
        public string? ReceiptNo { get; set; }
    }
}