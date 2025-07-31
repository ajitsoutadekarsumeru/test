namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetAccountsByIdsDto : DtoBridge
    {
        public string? Id { get; set; }
        public string? CustomId { get; set; }
        public string? AGREEMENTID { get; set; }
        public string? BRANCH { get; set; }
        public string? CUSTOMERID { get; set; }
        public string? CUSTOMERNAME { get; set; }
        public string? DispCode { get; set; }
        public string? PRODUCT { get; set; }
        public string? SubProduct { get; set; }
        public string? ProductGroup { get; set; }
        public DateTime? PTPDate { get; set; }
        public string? Region { get; set; }
        public string? LatestMobileNo { get; set; }
        public string? LatestEmailId { get; set; }
        public string? LatestLatitude { get; set; }
        public string? LatestLongitude { get; set; }

        public DateTime? LatestPTPDate { get; set; }
        public decimal? LatestPTPAmount { get; set; }

        public DateTime? LatestPaymentDate { get; set; }

        public DateTime? LatestFeedbackDate { get; set; }
        public string? LatestFeedbackId { get; set; }
        public string? BranchCode { get; set; }
        public string? ProductCode { get; set; }
        public string? GroupId { get; set; }
        public string? DueDate { get; set; }
        public string? SegmentId { get; set; }
        public string? TreatmentId { get; set; }
        public string? LenderId { get; set; }
        public string? CustomerPersona { get; set; }
        public bool IsDNDEnabled { get; set; }
        public decimal? BOM_POS { get; set; }
        public long? BUCKET { get; set; }
        public string? CITY { get; set; }
        public string? CURRENT_BUCKET { get; set; }
        public DateTime? AllocationOwnerExpiryDate { get; set; }
        public long? CURRENT_DPD { get; set; }
        public decimal? CURRENT_POS { get; set; }
        public string? DISBURSEDAMOUNT { get; set; }
        public decimal? EMI_OD_AMT { get; set; }
        public string? EMI_START_DATE { get; set; }
        public decimal? EMIAMT { get; set; }
        public decimal? INTEREST_OD { get; set; }
        public string? MAILINGMOBILE { get; set; }
        public string? MAILINGZIPCODE { get; set; }

        public int? MONTH { get; set; }
        public string? NO_OF_EMI_OD { get; set; }
        public string? NPA_STAGEID { get; set; }
        public decimal? PENAL_PENDING { get; set; }
        public decimal? PRINCIPAL_OD { get; set; }
        public string? REGDNUM { get; set; }
        public string? STATE { get; set; }
        public string? PAYMENTSTATUS { get; set; }

        public int? YEAR { get; set; }
        public string? AgencyId { get; set; }
        public string? CollectorId { get; set; }
        public string? TOS { get; set; }
        public string? TOTAL_ARREARS { get; set; }
        public string? OVERDUE_DATE { get; set; }
        public string? NEXT_DUE_DATE { get; set; }
        public string? Excess { get; set; }
        public string? LOAN_STATUS { get; set; }
        public string? OTHER_CHARGES { get; set; }
        public decimal? TOTAL_OUTSTANDING { get; set; }
        public string? OVERDUE_DAYS { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? TeleCallingAgencyId { get; set; }
        public string? TeleCallerId { get; set; }
        public string? AllocationOwnerId { get; set; }

        public DateTime? AgencyAllocationExpiryDate { get; set; }
        public DateTime? TeleCallerAgencyAllocationExpiryDate { get; set; }
        public DateTime? AgentAllocationExpiryDate { get; set; }
        public DateTime? CollectorAllocationExpiryDate { get; set; }
        public DateTime? TeleCallerAllocationExpiryDate { get; set; }
        public string? CO_APPLICANT1_NAME { get; set; }
        public string? NEXT_DUE_AMOUNT { get; set; }
        public bool Paid { get; set; }
        public bool Attempted { get; set; }
        public bool UnAttempted { get; set; }
        public string? Partner_Loan_ID { get; set; }
        public bool? IsEligibleForSettlement { get; set; }
        public bool? IsEligibleForRepossession { get; set; }
        public bool? IsEligibleForLegal { get; set; }
        public bool? IsEligibleForRestructure { get; set; }
        public string? EMAIL_ID { get; set; }
        public string? PAN_CARD_DETAILS { get; set; }
        public string? SCHEME_DESC { get; set; }
        public string? ZONE { get; set; }
        public string? CentreID { get; set; }
        public string? CentreName { get; set; }
        public string? GroupName { get; set; }
        public string? Area { get; set; }        
        public string? PRIMARY_CARD_NUMBER { get; set; }
        public string? BILLING_CYCLE { get; set; }
        public DateTime? LAST_STATEMENT_DATE { get; set; }
        public decimal? CURRENT_MINIMUM_AMOUNT_DUE { get; set; }
        public decimal? CURRENT_TOTAL_AMOUNT_DUE { get; set; }
        public string? RESIDENTIAL_CUSTOMER_CITY { get; set; }
        public string? RESIDENTIAL_CUSTOMER_STATE { get; set; }
        public string? RESIDENTIAL_PIN_CODE { get; set; }        
        public string? RESIDENTIAL_COUNTRY { get; set; }
        public DateTime LastUploadedDate { get; set; }
        public string? Latest_Number_From_Trail { get; set; }
        public string? Latest_Email_From_Trail { get; set; }
        public string? Latest_Address_From_Trail { get; set; }
        public string? Latest_Number_From_Receipt { get; set; }
        public string? Latest_Number_From_Send_Payment { get; set; }
        public DateTime? LatestAllocationDate { get; set; }
        public string? ReverseOfAgreementId { get; set; }
        public string? ReverseOfPrimaryCard { get; set; }
    }
}
