using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class AddPhysicalCollectionDto : DtoBridge
    {
        public string Referenceid { get; set; }

        [Required]
        public string CollectionMode { get; set; }

        public string? ContactType { get; set; }
        public string? CountryCode { get; set; }
        public string? AreaCode { get; set; }
        public string MobileNo { get; set; }
        public string EMailId { get; set; }
        public string? PayerImageName { get; set; }
        public string? CustomerName { get; set; }
        public string CollectorId { get; set; }
        public string? CollectorName { get; set; }
        public string? CollectorCode { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Please enter valid amount.")]
        public decimal? Amount { get; set; }

        //[Required] // Commented based on product owner JO approval
        //[StringLength(200, ErrorMessage = "The ReceiptNo must be at least 2 characters long.", MinimumLength = 2)]
        public string? PhysicalReceiptNumber { get; set; }

        [Required]
        public string AccountNo { get; set; }

        public ICollection<PaymentDocAPIModel> CollectionDocs { get; set; }
        public ChequeAPIModel? Cheque { get; set; }
        public CashAPIModel? Cash { get; set; }

        [StringLength(32)]
        public string? TransactionNumber { get; set; }

        public DateTime PhysicalReceiptDate { get; set; }
        public string? PaymentPartner { get; set; }

        [Required]
        public string yRelationshipWithCustomer { get; set; }

        public string? yPANNo { get; set; }
        public decimal? yForeClosureAmount { get; set; }
        public decimal? yOverdueAmount { get; set; }
        public decimal? yBounceCharges { get; set; }
        public decimal? yPenalInterest { get; set; }
        public decimal? Settlement { get; set; }
        public decimal? othercharges { get; set; }

        [StringLength(50)]
        public string? DepositAccountNumber { get; set; }

        [StringLength(50)]
        public string? DepositBankName { get; set; }

        [StringLength(50)]
        public string? DepositBankBranch { get; set; }

        public bool? IsPoolAccount { get; set; }
        public bool? IsDepositAccount { get; set; }
        public string? ReceiptType { get; set; }
        public bool? IsNewPhonenumber { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid amountBreakUp1.")]
        public decimal? amountBreakUp1 { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid amountBreakUp2.")]
        public decimal? amountBreakUp2 { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid amountBreakUp3.")]
        public decimal? amountBreakUp3 { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid amountBreakUp4.")]
        public decimal? amountBreakUp4 { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid amountBreakUp5.")]
        public decimal? amountBreakUp5 { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid amountBreakUp5.")]
        public decimal? amountBreakUp6 { get; set; }
    }
}