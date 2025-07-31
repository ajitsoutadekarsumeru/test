namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetAckBatchesDto : DtoBridge
    {
        public string? Id { get; set; }

        public decimal? Amount { get; set; }

        public string? ModeOfPayment { get; set; }

        public string? BatchCode { get; set; }

        public string? BankAccountNo { get; set; }

        public string? BankName { get; set; }

        public string? Receiptcount { get; set; }

        public string? ReceiptIssuedBy { get; set; }

        public DateTimeOffset? CreatedDate { get; set; }

        public string? BatchType { get; set; }
    }
}