namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetCollectionByIdDto : DtoBridge
    {
        public string Id { get; set; }
        public decimal? Amount { get; set; }
        public string? BatchId { get; set; }
        public string? CustomerAccountNo { get; set; }
        public decimal? EmiAmount { get; set; }
        public string? Product { get; set; }
        public string? BCC_PENDING { get; set; }
        public DateTime? ReceiptDate { get; set; }
        public string? CustomerName { get; set; }
        public string? CollectorName { get; set; }
        public string? CollectorId { get; set; }
        public string? CollectorCode { get; set; }
        public DateTime? ReceivedAtAgency { get; set; }
        public string? PaymentStatus { get; set; }
        public string? ReceiptNo { get; set; }
        public string? CollectionMode { get; set; }
        public string? MobileNo { get; set; }
        public string? EMailId { get; set; }
        public string? PayerImageName { get; set; }
        public string? ChangeRequestImageName { get; set; }
        public string? PhysicalReceiptNumber { get; set; }
        public string? BankName { get; set; }
        public string? BranchName { get; set; }
        public string? InstrumentNo { get; set; }
        public DateTime? InstrumentDate { get; set; }
        public string? MicrCode { get; set; }
        public string? yForeClosureAmount { get; set; }
        public string? yOverdueAmount { get; set; }
        public string? yBounceCharges { get; set; }
        public string? yPenalInterest { get; set; }
        public string? yRelationshipWithCustomer { get; set; }
        public string? yPANNo { get; set; }

        public string? DepositAccountNumber { get; set; }
        public string? DepositBankBranch { get; set; }
        public string? DepositBankName { get; set; }
        public bool? IsDepositAccount { get; set; }
        public bool? IsPoolAccount { get; set; }
        public string? BankCity { get; set; }
        public string? IfscCode { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }

        public string? ReceiptType { get; set; }
        public string? OtherChanges { get; set; }
        public string? Settlement { get; set; }
        public string? CollectionPickupCharge { get; set; }
        public string? EBCCharge { get; set; }
        public string? TransactionNumber { get; set; }
        public string? Remarks { get; set; }

        public decimal? POS { get; set; }
        public decimal? PTPAmount { get; set; }
        public DateTime? PTPDate { get; set; }
        public decimal? TOS { get; set; }
    }
}