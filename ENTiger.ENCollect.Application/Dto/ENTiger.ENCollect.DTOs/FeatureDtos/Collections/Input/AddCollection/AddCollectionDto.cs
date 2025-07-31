using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class AddCollectionDto : DtoBridge
    {
        public string? Referenceid { get; set; }
        public string CollectionMode { get; set; }

        public string? ContactType { get; set; }
        public string? CountryCode { get; set; }
        public string? AreaCode { get; set; }
        public string? MobileNo { get; set; }
        public string? EMailId { get; set; }
        public string? PayerImageName { get; set; }
        public string? CustomerName { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public ChequeAPIModel Cheque { get; set; }
        public CashAPIModel Cash { get; set; }
        public DateTime? OfflineCollectionDate { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Please enter valid amount.")]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "The ReceiptNo must be at least 2 characters long.", MinimumLength = 2)]
        public string? ReceiptNo { get; set; }
        public string AccountNo { get; set; }
        public ICollection<PaymentDocAPIModel> CollectionDocs { get; set; }

        [StringLength(32)]
        public string? TransactionNumber { get; set; }

        [StringLength(500, ErrorMessage = "{0} value cannot exceed {1} characters")]
        public string? Remarks { get; set; }

        public string? GeoLocation { get; set; }
        public string? PaymentPartner { get; set; }
        public bool IsMSL { get; set; }

        //----------------------
        [Required]
        public string yRelationshipWithCustomer { get; set; }

        public string yPANNo { get; set; }
        public decimal? yForeClosureAmount { get; set; }
        public decimal? yOverdueAmount { get; set; }
        public decimal? yBounceCharges { get; set; }
        public decimal? yPenalInterest { get; set; }
        public decimal? Settlement { get; set; }
        public decimal? othercharges { get; set; }

        //-------------------------------------
        [StringLength(50)]
        public string DepositAccountNumber { get; set; }

        [StringLength(50)]
        public string DepositBankName { get; set; }

        [StringLength(50)]
        public string DepositBankBranch { get; set; }

        public bool? IsPoolAccount { get; set; }
        public bool? IsDepositAccount { get; set; }
        public string ReceiptType { get; set; }
        public bool? IsNewPhonenumber { get; set; }
        public string creditCardNumber { get; set; }

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

    public class ChequeAPIModel
    {
        public string? BankName { get; set; }
        public string? BranchName { get; set; }
        public string? InstrumentNo { get; set; }
        public DateTime? InstrumentDate { get; set; }
        public string? MICRCode { get; set; }
        public string? IFSCCode { get; set; }
        public string? BankCity { get; set; }
    }

    public class PaymentDocAPIModel
    {
        [StringLength(500)]
        public string Path { get; set; }

        [StringLength(100)]
        public string FileName { get; set; }

        public long FileSize { get; set; }
    }

    public class CashAPIModel
    {
    }
}