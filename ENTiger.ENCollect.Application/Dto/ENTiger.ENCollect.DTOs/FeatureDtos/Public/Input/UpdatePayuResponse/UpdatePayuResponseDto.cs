namespace ENTiger.ENCollect.PublicModule
{
    public partial class UpdatePayuResponseDto : DtoBridge
    {
        public string? MihPayId { get; set; }
        public string? Mode { get; set; }
        public string? Status { get; set; }
        public string? Key { get; set; }
        public string? TxnId { get; set; }
        public decimal Amount { get; set; }
        public DateTime? AddedOn { get; set; }
        public string? ProductInfo { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? ZipCode { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Field1 { get; set; }
        public string? Field2 { get; set; }
        public string? Field9 { get; set; }
        public string? PaymentSource { get; set; }
        public string? PGType { get; set; }
        public string? Error { get; set; }
        public string? ErrorMessage { get; set; }
        public decimal NetAmountDebit { get; set; }
        public string? BankRefNo { get; set; }
        public string? Hash { get; set; }
        public string? BankCode { get; set; }
        public string? description { get; set; }
        public DateTime? SuccessAt { get; set; }
    }
}