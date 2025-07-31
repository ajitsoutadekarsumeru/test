namespace ENTiger.ENCollect
{
    public partial class BankAccountTypeDto : DtoBridge
    {
        public string? Id { get; set; }
        public string? Value { get; set; }

        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public bool Deleted { get; set; }
    }
}