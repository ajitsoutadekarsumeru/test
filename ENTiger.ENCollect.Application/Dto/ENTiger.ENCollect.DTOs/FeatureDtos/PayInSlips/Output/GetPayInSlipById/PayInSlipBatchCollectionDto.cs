namespace ENTiger.ENCollect.PayInSlipsModule
{
    public partial class PayInSlipBatchCollectionDto : DtoBridge
    {
        public string? ReceiptNo { get; set; }
        public decimal? Amount { get; set; }
        public string? BankName { get; set; }
        public string? BranchName { get; set; }
        public string? InstrumentNo { get; set; }
        public string? MICR { get; set; }
        public DateTime? InstrumentDate { get; set; }
        public string? IfscCode { get; set; }
    }
}