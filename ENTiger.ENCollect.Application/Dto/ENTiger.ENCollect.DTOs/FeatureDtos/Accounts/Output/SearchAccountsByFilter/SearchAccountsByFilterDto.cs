using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchAccountsByFilterDto : DtoBridge
    {
        public string? AccountNo { get; set; }
        public string? Address { get; set; }
        public string? Area { get; set; }
        public string? City { get; set; }
        public string? ContractId { get; set; }
        public string? CurrentBucket { get; set; }
        public long? CurrentDPD { get; set; }
        public string? CustomerId { get; set; }
        public string? CustomerName { get; set; }

        [StringLength(100)]
        public string? EMailId { get; set; }

        public decimal? EMIAmount { get; set; }
        public string? Id { get; set; }

        public string? MobileNo { get; set; }

        //public long? MonthStartingBucket { get; set; }
        public string? MonthStartingBucket { get; set; }

        public decimal? POS { get; set; }
        public string? ProductName { get; set; }
        public decimal? PTPAmount { get; set; }
        public DateTime? PTPDate { get; set; }
        public string? State { get; set; }
        public string? TOS { get; set; }
        public string? Branch { get; set; }
        public string? AccountCategory { get; set; }
        public string? SegmentationName { get; set; }
        public string? BranchCode { get; set; }
        public string? CentreID { get; set; }
        public string? GroupID { get; set; }
        public string? CentreName { get; set; }
        public string? GroupName { get; set; }
        public string? ProductGroup { get; set; }
        public string? SubProduct { get; set; }
        public string? AmountDue { get; set; }
        public DateTime? LastStatementDate { get; set; }
        public DateTime? LastStatementDueDate { get; set; }
        public string? CurrentBalanceOS { get; set; }
        public string? StatementedBucket { get; set; }
        public string? CreditCardNo { get; set; }
        public string? PermanentMobileNo { get; set; }
        public string? PartnerLoanId { get; set; }
        public string? CreditCardNumber { get; set; }
        public string? AccountStatus { get; set; }
        public string? PDD { get; set; }
        public string? TAD { get; set; }
        public string? MAD { get; set; }
        public string? Bucket { get; set; }
        public string? CurrentBalance { get; set; }
        public string? Cycle { get; set; }
        public int ExpiresInDays { get; set; }
        public DateTime? CollectorAllocationExpiryDate { get; set; }
        public string AllocationExpiryColor { get; set; } = "green";
    }
}