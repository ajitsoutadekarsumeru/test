namespace ENTiger.ENCollect.CollectionBatchesModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetBatchesToPrintDto : DtoBridge
    {
        public string Id { get; set; }
        public decimal? Amount { get; set; }
        public string? ModeOfPayment { get; set; }
        public string? ProductGroup { get; set; }
        public string? Agency { get; set; }

        // public string? AcknowledgedBy { get; set; }
        public string? BranchAcknowleged { get; set; }

        public string? status { get; set; }
        public string? BatchCode { get; set; }
        public string? BankAccountNo { get; set; }
        public string? BankName { get; set; }
        public string? Receiptcount { get; set; }
        public string? ReceiptIssuedBy { get; set; }
        public DateTimeOffset CreatedDate { get; set; }

        public string? BatchType { get; set; }
    }
}