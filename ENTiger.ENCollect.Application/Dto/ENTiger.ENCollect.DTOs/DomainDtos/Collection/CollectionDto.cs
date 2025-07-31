using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class CollectionDto : DtoBridge
    {
        [StringLength(100)]
        public string? CustomId { get; set; }

        public decimal? Amount { get; set; }

        [StringLength(5)]
        public string? CurrencyId { get; set; }

        public DateTime? CollectionDate { get; set; } = DateTime.Now;

        [StringLength(50)]
        public string? RecordNo { get; set; }

        [StringLength(50)]
        public string? CollectionMode { get; set; }

        [StringLength(50)]
        public string? MobileNo { get; set; }

        [StringLength(20)]
        public string? ContactType { get; set; }

        [StringLength(20)]
        public string? CountryCode { get; set; }

        [StringLength(20)]
        public string? AreaCode { get; set; }

        [StringLength(200)]
        public string? EMailId { get; set; }

        [StringLength(200)]
        public string? PayerImageName { get; set; }

        [StringLength(200)]
        public string? CustomerName { get; set; }

        [StringLength(200)]
        public string? ChangeRequestImageName { get; set; }

        [StringLength(50)]
        public string? PhysicalReceiptNumber { get; set; }

        [StringLength(50)]
        public string? Latitude { get; set; }

        [StringLength(50)]
        public string? Longitude { get; set; }

        [StringLength(500)]
        public string? Remarks { get; set; }

        [StringLength(32)]
        public string? AccountId { get; set; }

        public LoanAccountDto Account { get; set; }

        public ApplicationUserDto Collector { get; set; }

        public ApplicationUserDto AckingAgent { get; set; }

        public ReceiptDto Receipt { get; set; }

        //public ApplicationOrg CollectionOrg { get; set; }

        public CollectionBatchDto CollectionBatch { get; set; }

        public CashDto Cash { get; set; }

        public ChequeDto Cheque { get; set; }

        public int MailSentCount { get; set; }
        public int SMSSentCount { get; set; }

        [StringLength(32)]
        public string? TransactionNumber { get; set; }

        public DateTime? AcknowledgedDate { get; set; }

        //public CollectionWorkflowState CollectionWorkflowState { get; set; }

        [StringLength(500)]
        public string? CancellationRemarks { get; set; }

        public DateTime? OfflineCollectionDate { get; set; }

        [StringLength(500)]
        public string? GeoLocation { get; set; }

        [StringLength(500)]
        public string? EncredibleUserId { get; set; }

        [StringLength(50)]
        public string? yForeClosureAmount { get; set; }

        [StringLength(50)]
        public string? yOverdueAmount { get; set; }

        [StringLength(50)]
        public string? yBounceCharges { get; set; }

        [StringLength(50)]
        public string? othercharges { get; set; }

        [StringLength(50)]
        public string? yPenalInterest { get; set; }

        [StringLength(50)]
        public string? Settlement { get; set; }

        [StringLength(100)]
        public string? yRelationshipWithCustomer { get; set; }//

        [StringLength(50)]
        public string? yPANNo { get; set; }//

        [StringLength(50)]
        public string? yUploadSource { get; set; }//

        [StringLength(50)]
        public string? yBatchUploadID { get; set; }//

        public string? yTest { get; set; }//

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
        public string? ErrorMessgae { get; set; }
        public decimal? amountBreakUp1 { get; set; }
        public decimal? amountBreakUp2 { get; set; }
        public decimal? amountBreakUp3 { get; set; }
        public decimal? amountBreakUp4 { get; set; }
        public decimal? amountBreakUp5 { get; set; }
        public decimal? amountBreakUp6 { get; set; }
    }
}