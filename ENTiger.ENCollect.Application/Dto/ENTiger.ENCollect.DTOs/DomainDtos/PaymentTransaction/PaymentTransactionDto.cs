using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public class PaymentTransactionDto : DtoBridge
    {
        #region Attributes

        public PaymentGatewayDto PaymentGateway { get; set; }

        [StringLength(32)]
        public string PaymentGatewayID { get; set; }

        [StringLength(50)]
        public string MerchantReferenceNumber { get; set; }

        [StringLength(50)]
        public string MerchantTransactionId { get; set; }

        [StringLength(50)]
        public string BankTransactionId { get; set; }

        [StringLength(50)]
        public string BankReferenceNumber { get; set; }

        [StringLength(50)]
        public string BankId { get; set; }

        public decimal? Amount { get; set; }

        [StringLength(50)]
        public string Currency { get; set; }

        public DateTime? TransactionDate { get; set; }

        [StringLength(50)]
        public string StatusCode { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        public string ResponseMessage { get; set; }
        public string ErrorMessage { get; set; }

        [StringLength(50)]
        public string ErrorCode { get; set; }

        public bool? IsPaid { get; set; }

        [StringLength(50)]
        public string TransactionStatus { get; set; }

        [StringLength(50)]
        public string RRN { get; set; }

        [StringLength(50)]
        public string AuthCode { get; set; }

        [StringLength(50)]
        public string CardNumber { get; set; }

        [StringLength(50)]
        public string CardType { get; set; }

        [StringLength(50)]
        public string CardHolderName { get; set; }

        public LoanAccountDto LoanAccount { get; set; }

        [StringLength(32)]
        public string LoanAccountId { get; set; }

        #endregion Attributes
    }
}