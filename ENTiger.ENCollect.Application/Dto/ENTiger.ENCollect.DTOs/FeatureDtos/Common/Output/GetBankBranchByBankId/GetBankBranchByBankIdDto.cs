namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetBankBranchByBankIdDto : DtoBridge
    {
        public string? BankBranchId { get; set; }
        public string? BranchName { get; set; }
        public string? BranchCode { get; set; }
    }
}