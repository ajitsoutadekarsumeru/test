namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetDepositBankListDto : DtoBridge
    {
        public string? DepositBankName { get; set; }
        public string? DepositBranchName { get; set; }
        public string? DepositAccountNumber { get; set; }
        public string? AccountHolderName { get; set; }
    }
}