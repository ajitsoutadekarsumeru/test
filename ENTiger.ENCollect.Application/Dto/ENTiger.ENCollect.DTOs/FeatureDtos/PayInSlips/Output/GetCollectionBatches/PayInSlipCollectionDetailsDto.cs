namespace ENTiger.ENCollect.PayInSlipsModule
{
    public partial class PayInSlipCollectionDetailsDto : DtoBridge
    {
        public string? Amount { get; set; }
        public string? PaymentDate { get; set; }
        public string? ReceiptNo { get; set; }
        public string? CollecterCode { get; set; }
        public string? CollecterName { get; set; }
        public string? PaymentMode { get; set; }
        public ChequeDetailsDto? Denominationdetails { get; set; }
    }
}