namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetDepositSlipDetailsDto : DtoBridge
    {
        public string? Id { get; set; }
        public DateTime ReceiptDate { get; set; }
        public string? ReceiptNo { get; set; }
        public string? CustomerAccount { get; set; }
        public string? CustomerName { get; set; }
        public string? Amount { get; set; }
        public string? ModeOfPayment { get; set; }
        public string? DraweeBankName { get; set; }
        public string? DraweeBranchName { get; set; }
        public string? InstrumentNo { get; set; }
        public string? MICR { get; set; }
        public string? IFSC { get; set; }
    }
}