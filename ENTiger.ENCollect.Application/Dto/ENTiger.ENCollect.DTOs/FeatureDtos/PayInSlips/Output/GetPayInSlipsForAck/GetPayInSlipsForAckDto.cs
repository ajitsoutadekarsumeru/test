namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetPayInSlipsForAckDto : DtoBridge
    {
        public string Id { get; set; }
        public string CMSPayInSlipId { get; set; }
        public string ENCollectPayInSlipNumber { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public DateTime? DateOfDeposit { get; set; }
        public string BankAccountNo { get; set; }
        public string AccountHolderName { get; set; }
        public string ModeOfPayment { get; set; }
        public decimal Amount { get; set; }
        public string ProductGroup { get; set; }
        public string PayinSlipType { get; set; }
        public List<PayInSlipBatchIdDto> BatchIds { get; set; }
    }
}