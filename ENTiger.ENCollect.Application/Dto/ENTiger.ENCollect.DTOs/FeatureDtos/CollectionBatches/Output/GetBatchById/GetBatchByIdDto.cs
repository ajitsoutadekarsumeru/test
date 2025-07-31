namespace ENTiger.ENCollect.CollectionBatchesModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetBatchByIdDto : DtoBridge
    {
        public CollectionBatchOutputAPIModel CollectionBatch { get; set; }

        public ICollection<CollectionDetailsOutputApiModel> CollectionDetails { get; set; }
    }

    public class CollectionBatchOutputAPIModel : DtoBridge
    {
        public string? Id { get; set; }

        public decimal? Amount { get; set; }

        public string? ModeOfPayment { get; set; }

        public string? ProductGroup { get; set; }

        public string? Agency { get; set; }

        public string? AcknowledgedByName { get; set; }

        public string? BranchAcknowleged { get; set; }

        public string? status { get; set; }

        public string? BatchCode { get; set; }

        public string? BankAccountNo { get; set; }

        public string? BankName { get; set; }

        public string? Receiptcount { get; set; }

        public string? ReceiptIssuedBy { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public string? BatchType { get; set; }

        public string? CustomId { get; set; }
    }

    public class CollectionDetailsOutputApiModel : DtoBridge
    {
        public string? Id { get; set; }

        public string? ReceiptNo { get; set; }

        public string? collectorId { get; set; }

        public string? collectorFistName { get; set; }

        public string? collectorMiddleName { get; set; }

        public string? DepositAccountNumber { get; set; }

        public string? DepositBankName { get; set; }

        public string? DepositBankBranch { get; set; }

        public string? collectorLastName { get; set; }

        public string? CustomerName { get; set; }

        public string? AccountNo { get; set; }

        public string? PaymentMode { get; set; }

        public decimal? Amount { get; set; }

        public string? receiptDate { get; set; }

        public string? product { get; set; }

        public DateTime? receivedAtAgency { get; set; }

        public string? BatchId { get; set; }

        public string? Status { get; set; }

        public string? InstrumentNo { get; set; }

        public DateTime? InstrumentDate { get; set; }

        public string? DraweeBank { get; set; }

        public string? DraweeBranch { get; set; }

        public string? OverdueAmount { get; set; }

        public string? ForeclosureAmount { get; set; }

        public string? IfSCCode { get; set; }

        public string? MircCode { get; set; }

        public string? depositBranchName { get; set; }

        public string? BounceCharges { get; set; }

        public string? PenalAmount { get; set; }

        public string? OtherCharges { get; set; }

        public string? Settlement { get; set; }

        public string? EBCCharge { get; set; }

        public string? CollectionPickupCharge { get; set; }

        public string? TransactionNumber { get; set; }
    }

    public class CollectionBatchGetById : DtoBridge
    {
    }
}