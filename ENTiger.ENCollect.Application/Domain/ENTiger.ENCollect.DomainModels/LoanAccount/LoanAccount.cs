using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    [Table("LoanAccounts")]
    public partial class LoanAccount : DomainModelBridge
    {
        protected readonly ILogger<LoanAccount> _logger;

        public LoanAccount()
        {
        }

        public LoanAccount(ILogger<LoanAccount> logger)
        {
            _logger = logger;
        }

        #region "Attributes"



        #region "Protected"

        [StringLength(100)]
        public string? CustomId { get; set; }

        [StringLength(50)]
        public string? AGREEMENTID { get; set; }

        [StringLength(100)]
        public string? BRANCH { get; set; }

        [StringLength(50)]
        public string? CUSTOMERID { get; set; }

        [StringLength(100)]
        public string? CUSTOMERNAME { get; set; }

        [StringLength(100)]
        public string? DispCode { get; set; }

        [StringLength(100)]
        public string? PRODUCT { get; set; }

        [StringLength(100)]
        public string? SubProduct { get; set; }

        [StringLength(100)]
        public string? ProductGroup { get; set; }

        public DateTime? PTPDate { get; set; }

        [StringLength(100)]
        public string? Region { get; set; }

        [StringLength(50)]
        public string? LatestMobileNo { get; set; }

        [StringLength(50)]
        public string? LatestEmailId { get; set; }

        [StringLength(20)]
        public string? LatestLatitude { get; set; }

        [StringLength(20)]
        public string? LatestLongitude { get; set; }

        public DateTime? LatestPTPDate { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal? LatestPTPAmount { get; set; }

        public DateTime? LatestPaymentDate { get; set; }

        public DateTime? LatestFeedbackDate { get; set; }

        [StringLength(32)]
        public string? LatestFeedbackId { get; set; }

        [StringLength(50)]
        public string? BranchCode { get; set; }

        [StringLength(50)]
        public string? ProductCode { get; set; }

        [StringLength(50)]
        public string? GroupId { get; set; }

        [StringLength(100)]
        public string? DueDate { get; set; }

        [StringLength(32)]
        public string? SegmentId { get; set; }

        public Segmentation Segment { get; set; }

        [StringLength(32)]
        public string? TreatmentId { get; set; }

        public Treatment Treatment { get; set; }

        [StringLength(32)]
        public string? LenderId { get; set; }

        public CategoryItem Lender { get; set; }

        [StringLength(200)]
        public string? CustomerPersona { get; set; }

        public bool IsDNDEnabled { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? BOM_POS { get; set; }

        public long? BUCKET { get; set; }

        [StringLength(100)]
        public string? CITY { get; set; }

        [StringLength(50)]
        public string? CURRENT_BUCKET { get; set; }

        public DateTime? AllocationOwnerExpiryDate { get; set; }

        public long? CURRENT_DPD { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? CURRENT_POS { get; set; }

        [StringLength(50)]
        public string? DISBURSEDAMOUNT { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? EMI_OD_AMT { get; set; }

        [StringLength(50)]
        public string? EMI_START_DATE { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? EMIAMT { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? INTEREST_OD { get; set; }

        [StringLength(50)]
        public string? MAILINGMOBILE { get; set; }

        [StringLength(50)]
        public string? MAILINGZIPCODE { get; set; }

        public int? MONTH { get; set; }

        [StringLength(50)]
        public string? NO_OF_EMI_OD { get; set; }

        [StringLength(50)]
        public string? NPA_STAGEID { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? PENAL_PENDING { get; set; }

        public decimal? PRINCIPAL_OD { get; set; }

        [StringLength(100)]
        public string? REGDNUM { get; set; }

        [StringLength(100)]
        public string? STATE { get; set; }

        [StringLength(50)]
        public string? PAYMENTSTATUS { get; set; }

        public int? YEAR { get; set; }

        [StringLength(32)]
        public string? AgencyId { get; set; }

        public ApplicationOrg Agency { get; set; }

        [StringLength(32)]
        public string? CollectorId { get; set; }

        public ApplicationUser Collector { get; set; }

        [StringLength(50)]
        public string? TOS { get; set; }

        [StringLength(50)]
        public string? TOTAL_ARREARS { get; set; }

        [StringLength(50)]
        public string? OVERDUE_DATE { get; set; }

        [StringLength(50)]
        public string? NEXT_DUE_DATE { get; set; }

        [StringLength(50)]
        public string? Excess { get; set; }

        [StringLength(100)]
        public string? LOAN_STATUS { get; set; }

        [StringLength(100)]
        public string? OTHER_CHARGES { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? TOTAL_OUTSTANDING { get; set; }

        [StringLength(100)]
        public string? OVERDUE_DAYS { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [StringLength(32)]
        public string? TeleCallingAgencyId { get; set; }

        public ApplicationOrg TeleCallingAgency { get; set; }

        [StringLength(32)]
        public string? TeleCallerId { get; set; }

        public ApplicationUser TeleCaller { get; set; }

        [StringLength(32)]
        public string? AllocationOwnerId { get; set; }

        public ApplicationUser AllocationOwner { get; set; }

        public DateTime? AgencyAllocationExpiryDate { get; set; }

        public DateTime? TeleCallerAgencyAllocationExpiryDate { get; set; }

        public DateTime? AgentAllocationExpiryDate { get; set; }

        public DateTime? CollectorAllocationExpiryDate { get; set; }

        public DateTime? TeleCallerAllocationExpiryDate { get; set; }

        [StringLength(100)]
        public string? CO_APPLICANT1_NAME { get; set; }

        [StringLength(50)]
        public string? NEXT_DUE_AMOUNT { get; set; }

        public bool Paid { get; set; } = false;

        public bool Attempted { get; set; } = false;

        [StringLength(50)]
        public string? Partner_Loan_ID { get; set; }

        //----------------------------------------
        public bool? IsEligibleForSettlement { get; set; }

        public bool? IsEligibleForRepossession { get; set; }
        public bool? IsEligibleForLegal { get; set; }
        public bool? IsEligibleForRestructure { get; set; }

        [StringLength(50)]
        public string? EMAIL_ID { get; set; }

        [StringLength(25)]
        public string? PAN_CARD_DETAILS { get; set; }

        [StringLength(80)]
        public string? SCHEME_DESC { get; set; }

        [StringLength(80)]
        public string? ZONE { get; set; }

        [StringLength(50)]
        public string? CentreID { get; set; }

        [StringLength(50)]
        public string? CentreName { get; set; }

        [StringLength(50)]
        public string? GroupName { get; set; }

        [StringLength(200)]
        public string? Area { get; set; }

        [StringLength(25)]
        public string? PRIMARY_CARD_NUMBER { get; set; }

        [StringLength(2)]
        public string? BILLING_CYCLE { get; set; }

        public DateTime? LAST_STATEMENT_DATE { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal? CURRENT_MINIMUM_AMOUNT_DUE { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal? CURRENT_TOTAL_AMOUNT_DUE { get; set; }

        [StringLength(30)]
        public string? RESIDENTIAL_CUSTOMER_CITY { get; set; }

        [StringLength(30)]
        public string? RESIDENTIAL_CUSTOMER_STATE { get; set; }

        [StringLength(10)]
        public string? RESIDENTIAL_PIN_CODE { get; set; }

        [StringLength(25)]
        public string? RESIDENTIAL_COUNTRY { get; set; }

        public DateTime LastUploadedDate { get; set; }

        [StringLength(50)]
        public string? Latest_Number_From_Trail { get; set; }

        [StringLength(50)]
        public string? Latest_Email_From_Trail { get; set; }

        [StringLength(50)]
        public string? Latest_Address_From_Trail { get; set; }

        [StringLength(50)]
        public string? Latest_Number_From_Receipt { get; set; }

        [StringLength(50)]
        public string? Latest_Number_From_Send_Payment { get; set; }

        public LoanAccountJSON? AccountJSON { get; set; }

        public DateTime? LatestAllocationDate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string? ReverseOfAgreementId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string? ReverseOfPrimaryCard { get; set; }

        public List<Settlement> Settlements { get; set; }

        public List<LoanAccountsProjection> Projections { get; set; }

        // Navigation property to all geo mappings (one per geo level)

        public virtual ICollection<AccountProductMap> ProductMappings { get; set; } = new List<AccountProductMap>();
        public virtual ICollection<AccountGeoMap> GeoMappings { get; set; } = new List<AccountGeoMap>();

        public List<AccountContactHistory> _accountContactHistory { get; set; } = new();

        public void AddAccountContactHistory(AccountContactHistory accountContactHistory)
        {
               if (accountContactHistory == null) throw new ArgumentNullException(nameof(accountContactHistory));


            _accountContactHistory.Add(accountContactHistory);
            this.SetAddedOrModified(); // notify EF if required by your domain base class
        }

        #endregion "Protected"

        #endregion "Attributes"
    }
}