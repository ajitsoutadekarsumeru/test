namespace ENTiger.ENCollect
{
    public partial class CreditAccountDetailsDto : DtoBridge
    {
        public string? Id { get; set; }
        public string? AccountHolderName { get; set; }
        public string? BankAccountNo { get; set; }
        public string? BankBranchId { get; set; }
        public string? MICR { get; set; }
        public string? BankId { get; set; }
        public string? BankName { get; set; }
        public string? IfscCode { get; set; }
    }
}