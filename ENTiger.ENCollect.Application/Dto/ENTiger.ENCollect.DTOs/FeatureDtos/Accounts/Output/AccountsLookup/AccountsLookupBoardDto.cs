namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AccountsLookupDto : DtoBridge
    {
        public string? Id { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerID { get; set; }
        public string? CustomerAccountNo { get; set; }
        public string? Product { get; set; }
        public string? CurrentBucket { get; set; }
        public long? MonthOpeningBucket { get; set; }
        public decimal? EmiAmount { get; set; }
        public decimal? MonthOpeningPOS { get; set; }
        public string? AgencyCode { get; set; }
        public decimal? OverdueEmiAmount { get; set; }
        public string? NoOfEMIDue { get; set; }
        public decimal? CurrentPOS { get; set; }
        public string? NpaStageID { get; set; }
        public string? CollectorName { get; set; }
        public string? TeleCallingAgencyCode { get; set; }
        public string? TeleCallerName { get; set; }
        public string? SegmentationName { get; set; }
        public string? TreatmentName { get; set; }
        public string? SegmentationId { get; set; }
        public string? TreatmentId { get; set; }
        public string? CustCardNo { get; set; }
        public string? MinimumAmountDue { get; set; }
        public string? StatementedClosingBalance { get; set; }
        public string? AgencyName { get; set; }
        public string? AllocationOwnerName { get; set; }
        public string? AllocationOwnerCode { get; set; }
        public string? PartnerLoanId { get; set; }
        public string? CreditCardNumber { get; set; }
        public string? AccountStatus { get; set; }
        public string? PDD { get; set; }
        public string? TAD { get; set; }
        public string? MAD { get; set; }
        public string? Bucket { get; set; }
        public string? CurrentBalance { get; set; }
        public string? Cycle { get; set; }
        public long? Current_DPD { get; set; }
        public string? TotalOverdueAmount { get; set; }
        public string? ProductGroup { get; set; }
        public string? AccountJSON { get; set; }
    }
}