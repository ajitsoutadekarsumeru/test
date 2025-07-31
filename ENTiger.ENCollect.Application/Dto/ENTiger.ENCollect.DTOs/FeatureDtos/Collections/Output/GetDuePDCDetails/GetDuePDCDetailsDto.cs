namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetDuePDCDetailsDto : DtoBridge
    {
        public string? Id { get; set; }

        public DateTime? InstrumentDate { get; set; }

        public string? ReceiptNo { get; set; }

        public string? CustomerName { get; set; }

        public string? CustomerAccountNo { get; set; }

        public decimal? Amount { get; set; }

        public string? PaymentStatus { get; set; }
    }
}