namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetPayInSlipByIdDto : DtoBridge
    {
        public string? Id { get; set; }

        public string? PayInSlipNo { get; set; }

        public string? CMSPayInSlipNo { get; set; }

        public string? ModeOfPayment { get; set; }

        public string? BankAccountNo { get; set; }

        public string? BankName { get; set; }

        public DateTime? DateOfDeposit { get; set; }

        public decimal Amount { get; set; }

        public string? Latitude { get; set; }

        public string? Longitude { get; set; }

        public bool? IsPrinted { get; set; }

        public string? PrintedById { get; set; }

        public DateTime? PrintedDate { get; set; }

        public string? CreatedById { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? PayInSlipImageName { get; set; }

        public ICollection<PayInSlipBatchCollectionDto> PayInSlipCollectionList { get; set; }

        public string? PayinslipType { get; set; }

        public string? AccountHolderName { get; set; }

        public List<PayInSlipBatchDto> BatchIds { get; set; }

        public string? DepositSlipBranchName { get; set; }

        public string? ProductGroup { get; set; }
    }
}