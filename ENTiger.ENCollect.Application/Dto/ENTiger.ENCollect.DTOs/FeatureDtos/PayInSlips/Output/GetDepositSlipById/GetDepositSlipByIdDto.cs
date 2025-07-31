namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetDepositSlipByIdDto : DtoBridge
    {
        public string? ID { get; set; }

        public string? CMSPayInSlipNo { get; set; }

        public string? PayInSlipNo { get; set; }

        public string? BankName { get; set; }

        public string? BranchName { get; set; }

        public DateTime? DateOfDeposit { get; set; }

        public string? BankAccountNo { get; set; }

        public string? AccountHolderName { get; set; }

        public string? ModeOfPayment { get; set; }

        public decimal Amount { get; set; }

        public string? Latitude { get; set; }

        public string? Longitude { get; set; }

        public string? DepositeBankName { get; set; }

        public string? DepositeBranchName { get; set; }

        public string? DepositeAccountNickName { get; set; }

        public string? PayInSlipImageName { get; set; }

        public List<DepositSlipBatchCollectionDto> ReceiptDetails { get; set; }

        public string? ProductGroup { get; set; }
    }
}