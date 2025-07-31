using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetTeleCallerAccountDetailsDto : DtoBridge
    {
        public TelecallerAccountOutputApiModel Account { get; set; }
        public ICollection<TelecallerDashboardCollectionsOutputApiModel> CollectionHistory { get; set; }
        public ICollection<TelecallerDashboardFeedBacksHistoryOutputApiModel> FeedBackHistory { get; set; }
        public ICollection<LoanAccountNotesOutputApiModel> Notes { get; set; }
        public ICollection<TelecallerAccountCommunicationsOutputModel> Communications { get; set; }
        public ICollection<LoanAccountFlagsOutputApiModel> Flags { get; set; }
        public ICollection<ChequeDetailsOutputApiModel> ChequeRejected { get; set; }
        public ICollection<ChequeDetailsOutputApiModel> Cheques { get; set; }
        public ICollection<ENCollectLoanAccountCoApplicants> CoApplicants { get; set; }
        public ICollection<ENCollectLoanAccountSecurities> Securities { get; set; }
        public ICollection<ENCollectLoanAccountDisbursements> Disbursements { get; set; }
        public ICollection<ENCollectLoanAccountCharges> Charges { get; set; }
        public ICollection<ENCollectLoanAccountsProjection> Transactions { get; set; }
        public AllocationHistoryOutputApiModel Allocations { get; set; }
        public ENCollectCreditBureauDetails CreditScore { get; set; }
    }

    public class TelecallerAccountOutputApiModel
    {
        public string? AccountJSON { get; set; }
        public TelecallerAccountDetailsOutputApiModel CaseDetails { get; set; }
        public TelecallerAccountAnalyticsOutputAPIModel Analytics { get; set; }
        public TelecallerAccountFinancialsOutputApiModel CaseFinancials { get; set; }
        public TelecallerAccountStatusOutputApiModel CaseStatus { get; set; }
        public TelecallerAccountDemographicOutputApiModel DemographicDetails { get; set; }
        public TelecallerAccountOtherDetailsOutputAPIModel OtherDetails { get; set; }
        public TelecallerAccountOtherContactDetailsOutputAPIModel OtherContactDetails { get; set; }
        public TelecallerAccountAssetDetailsOutputAPIModel AssetDetails { get; set; }
        public AccountLatestContactDetailsDto ContactDetails { get; set; }
    }

    public class TelecallerAccountDetailsOutputApiModel
    {
        public string? AGREEMENTID { get; set; }
        public string? LenderId { get; set; }
        public string? Partner_Loan_ID { get; set; }
        public string? Partner_Customer_ID { get; set; }
        public string? Partner_Name { get; set; }
        public string? CUSTOMERID { get; set; }
        public string? CUSTOMERNAME { get; set; }
        public string? ProductGroup { get; set; }
        public string? ProductCode { get; set; }
        public string? PRODUCT { get; set; }
        public string? SubProduct { get; set; }
        public string? BANK_ACC_NUM { get; set; }
        public string? BANK_CODE { get; set; }
        public string? BANK_NAME { get; set; }
        public string? SanctionDate { get; set; }
        public string? LOAN_DATE { get; set; }
        public string? DISBURSALDATE { get; set; }
        public string? NEXT_DUE_DATE { get; set; }
        public string? TOTAL_PAID_TO_DATE { get; set; }
        public string? LAST_PAYMENT_DATE { get; set; }
        public string? DueDate { get; set; }
        public string? REPAYMENT_STARTDATE { get; set; }
        public string? WRITEOFFDATE { get; set; }
        public string? EMI_START_DATE { get; set; }
        public string? CHARGEOFF_DATE { get; set; }
        public string? OVERDUE_DATE { get; set; }
        public string? MODE_OF_OPERATION { get; set; }
        public string? LOCATION_CODE { get; set; }
        public string? SCHEME_DESC { get; set; }
        public string? USER_CLASSIFICATION_DATE { get; set; }
        public string? MAIN_CLASSIFICATION_USER { get; set; }
        public string? ECS_ENABLED { get; set; }
        public string? WRITE_OFF_AMOUNT { get; set; }
        public bool? IsEligibleForSettlement { get; set; }
        public bool? IsEligibleForLegal { get; set; }
        public bool? IsEligibleForRepossession { get; set; }
        public bool? IsEligibleForRestructure { get; set; }
        public string? RephasementFlag { get; set; }
    }

    public class TelecallerAccountAnalyticsOutputAPIModel
    {
        public string? CustomerPersona { get; set; }
        public DateTime? PTPDate { get; set; }
        public string? DELSTRING { get; set; }
        public string? PropensityToPay { get; set; }
        public string? PropensityToPayConfidence { get; set; }
        public bool IsDNDEnabled { get; set; }
        public string? DispCode { get; set; }
        public string? SegmentationCode { get; set; }
        public string? PAR { get; set; }
        public string? CreditBureauScore { get; set; }
        public string? CustomerBehaviourScore1 { get; set; }
        public string? CustomerBehaviourScore2 { get; set; }
        public string? EarlyWarningScore { get; set; }
        public string? CustomerBehaviorScoreToKeepHisWord { get; set; }
        public string? PreferredModeOfPayment { get; set; }
        public string? PropensityToPayOnline { get; set; }
        public string? DigitalContactabilityScore { get; set; }
        public string? CallContactabilityScore { get; set; }
        public string? FieldContactabilityScore { get; set; }
        public string? PreferredLanguageId { get; set; }
        public string? EWS_Score { get; set; }
    }

    public class TelecallerAccountFinancialsOutputApiModel
    {
        public string? BCC_PENDING { get; set; }
        public string? ASSET_LOAN_AMOUNT { get; set; }
        public decimal? EMIAMT { get; set; }
        public string? PRINCIPAL_OUTSTANDING { get; set; }
        public string? TOTAL_INTEREST { get; set; }
        public decimal? INTEREST_OD { get; set; }
        public string? INTEREST_AMOUNT { get; set; }
        public string? PENAL_ST { get; set; }
        public string? OtherchargesOverdue { get; set; }
        public decimal? PENAL_PENDING { get; set; }
        public string? LOAN_LIABILITY { get; set; }
        public decimal? EMI_OD_AMT { get; set; }
        public decimal? CURRENT_POS { get; set; }
        public string? NEXT_DUE_AMOUNT { get; set; }
        public string? BOUNCED_CHEQUE_CHARGES { get; set; }
        public string? LAST_PAYMENT_AMOUNT { get; set; }
        public decimal? PRINCIPAL_OD { get; set; }
        public decimal? BOM_POS { get; set; }
        public string? SACT_AMOUNT { get; set; }
        public string? DISBURSEDAMOUNT { get; set; }
        public string? LOAN_AMOUNT { get; set; }
        public string? OTHER_CHARGES { get; set; }
        public string? ForeclosePrepayCharge { get; set; }
        public string? TOS { get; set; }
        public string? TOTAL_ARREARS { get; set; }
        public string? OtherReceivables { get; set; }
        public string? Excess { get; set; }
        public string? TOTAL_POS { get; set; }
        public string? TOTAL_BOUNCE_CHARGE { get; set; }
        public string? TOTAL_LATE_PAYMENT_CHARGE { get; set; }
        public string? TOTAL_INSURANCE_CHARGE { get; set; }
        public string? TOTAL_PROCESSING_CHARGE { get; set; }
        public string? TOTAL_VALUATION_CHARGE { get; set; }
        public string? OTHER_CHARGE { get; set; }
        public string? INT_AMOUNT { get; set; }
        public string? TOTAL_OVERDUE_AMT { get; set; }
        public string? LEGAL_CHARGE { get; set; }
        public string? PAYMENT_TYPE { get; set; }
    }

    public class TelecallerAccountStatusOutputApiModel
    {
        public string? OVERDUE_DAYS { get; set; }
        public long? CURRENT_DPD { get; set; }
        public string? CURRENT_BUCKET { get; set; }
        public long? BUCKET { get; set; }
        public string? NPA_STAGEID { get; set; }
        public string? PAYMENTSTATUS { get; set; }
        public string? Month_on_Books { get; set; }
        public string? NO_OF_EMI_OD { get; set; }
        public string? LoanPeriodInDays { get; set; }
        public string? TENURE_MONTH { get; set; }
        public string? Tenure_Days { get; set; }
        public string? LOAN_STATUS { get; set; }
    }

    public class TelecallerAccountOtherContactDetailsOutputAPIModel
    {
        public string? PERMANENT_COUNTRY_CODE { get; set; }
        public string? PinCode { get; set; }
        public string? NONMAILINGADDRESS { get; set; }
        public string? ADDRESSTYPE2 { get; set; }
        public string? EMPLOYER_CITY_CODE { get; set; }
        public string? EMPLOYER_STATE_CODE { get; set; }
        public string? GUARANTOR1_CITY { get; set; }
        public string? GUARANTOR1_COUNTY { get; set; }
        public string? FATHERS_ADDRS { get; set; }
        public string? MOTHERS_ADDRS { get; set; }
        public string? EMPLOYER_ADDRESS { get; set; }
        public string? ADD_ON_ADDRESS_1 { get; set; }
        public string? REFERENCE1_ADDRS { get; set; }
        public string? REF1_ADDRS { get; set; }
        public string? REF2_ADDRS { get; set; }
        public string? NONMAILINGCITY { get; set; }
        public string? NONMAILINGLANDMARK { get; set; }
        public string? NONMAILINGSTATE { get; set; }
        public string? NONMAILINGZIPCODE { get; set; }
        public string? CUSTOMER_EMPLOYER { get; set; }
        public string? CUSTOMER_OFFICE_COUNTRY { get; set; }
        public string? CUSTOMER_OFFICE_PIN_CODE { get; set; }
        public string? RESIDENTIAL_COUNTRY { get; set; }
        public string? RESIDENTIAL_CUSTOMER_CITY { get; set; }
        public string? RESIDENTIAL_CUSTOMER_STATE { get; set; }
        public string? RESIDENTIAL_PIN_CODE { get; set; }
    }

    public class TelecallerAccountDemographicOutputApiModel
    {
        public DateTime? DateOfBirth { get; set; }
        public string? GENDER { get; set; }
        public string? EMAIL_ID { get; set; }
        public string? PAN_CARD_DETAILS { get; set; }
        public string? MaritalStatus { get; set; }
        public string? SZ_SPOUSE_NAME { get; set; }
        public string? NameOfBusiness { get; set; }
        public string? RegistrationNo { get; set; }
        public string? TAXIdentificationNumber { get; set; }
        public string? Area { get; set; }
        public string? COMMUNICATION_COUNTRY_CODE { get; set; }
        public string? ZONE { get; set; }
        public string? Region { get; set; }
        public string? STATE { get; set; }
        public string? CITY { get; set; }
        public string? CityTier { get; set; }
        public string? BRANCH { get; set; }
        public string? BranchCode { get; set; }
        public string? BRANCH_CODE { get; set; }
        public string? MAILINGADDRESS { get; set; }
        public string? MAILINGZIPCODE { get; set; }
        public string? MAILINGPHONE1 { get; set; }
        public string? MAILINGPHONE2 { get; set; }
        public string? MAILINGMOBILE { get; set; }
        public string? ADDRESSTYPE1 { get; set; }
        public string? FATHERS_CONTACTS { get; set; }
        public string? GUARANTOR_1 { get; set; }
        public string? GUARANTOR_2 { get; set; }
        public string? GUARANTOR_3 { get; set; }
        public string? GUARANTOR_4 { get; set; }
        public string? CO_APPLICANT1_NAME { get; set; }
        public string? CO_APPLICANT1_CONTACT { get; set; }
        public string? CO_APPLICANT_1 { get; set; }
        public string? CO_APPLICANT_2 { get; set; }
        public string? CO_APPLICANT_3 { get; set; }
        public string? CO_APPLICANT_4 { get; set; }
        public string? FATHERNAME { get; set; }
        public string? MOTHERS_CONTACTS { get; set; }
        public string? MOTHERS_NAME { get; set; }
        public string? REF1_CONTACT { get; set; }
        public string? REFERENCE1_NAME { get; set; }
        public string? REFERENCE2_NAME { get; set; }
        public string? NONMAILINGPHONE1 { get; set; }
        public string? NONMAILINGPHONE2 { get; set; }
        public string? REF2_CONTACT { get; set; }
        public string? NONMAILINGMOBILE { get; set; }
        public string? CUSTOMERS_PERMANENT_EMAIL_ID { get; set; }
        public string? NewAddress { get; set; }
        public string? LatestMobileNo { get; set; }
    }

    public class TelecallerAccountAssetDetailsOutputAPIModel
    {
        public string? PHYSICAL_ADDRESS { get; set; }
        public int? MONTH { get; set; }
        public int? YEAR { get; set; }
        public string? MAKE { get; set; }
        public string? CHASISNUM { get; set; }
        public string? ENGINENUM { get; set; }
        public string? REGISTRATION { get; set; }
        public string? MODELID { get; set; }
        public string? MANUFACTURERDESC { get; set; }
        public string? REGDNUM { get; set; }
        public string? UNIT_DESC { get; set; }
        public string? MARKET_VALUE { get; set; }
        public string? SECURITY_TYPE { get; set; }
        public string? Level_Desc { get; set; }
        public string? AssetType { get; set; }
        public string? AssetDetails { get; set; }
        public string? AssetName { get; set; }
        public string? GroupId { get; set; }
        public string? GroupName { get; set; }
        public string? CentreID { get; set; }
        public string? CentreName { get; set; }
        public string? CROName { get; set; }
        public string? SalesPointCode { get; set; }
        public string? SALSEPOINTNAME { get; set; }
        public string? BUILDING_VALUATION { get; set; }
        public string? VALUATION_DATE { get; set; }
        public string? PropertyVerificationTag { get; set; }
    }

    public class TelecallerAccountOtherDetailsOutputAPIModel
    {
        public string? UDF1 { get; set; }
        public string? UDF2 { get; set; }
        public string? UDF3 { get; set; }
        public string? UDF4 { get; set; }
        public string? UDF5 { get; set; }
        public string? UDF6 { get; set; }
        public string? UDF7 { get; set; }
        public string? UDF8 { get; set; }
        public string? UDF9 { get; set; }
        public string? UDF10 { get; set; }
        public string? UDF11 { get; set; }
        public string? UDF12 { get; set; }
        public string? UDF13 { get; set; }
        public string? UDF14 { get; set; }
        public string? UDF15 { get; set; }
        public string? UDF16 { get; set; }
        public string? UDF17 { get; set; }
        public string? UDF18 { get; set; }
        public string? UDF19 { get; set; }
        public string? UDF20 { get; set; }
    }

    public class TelecallerDashboardCollectionsOutputApiModel
    {
        public string? Id { get; set; }//colection.paymentdate
        public string? ReceiptNo { get; set; }
        public DateTime? PaymentDate { get; set; }//colection.paymentdate
        public decimal? Amount { get; set; }//c.amount
        public string? CollectionMode { get; set; }//c.paymentmode

        public string? InstrumentNumber { get; set; }
        public string? InstrumentDate { get; set; }
        public string? BankName { get; set; }
        public string? BranchName { get; set; }//collection.cheque

        public string? CollectorFirstName { get; set; }//c.collector.fname
        public string? CollectorLastName { get; set; }//c.collector.fname
        public string? PaymentStatus { get; set; }//lc.status
        public string? PayerImageName { get; set; }//lc.status
        public string? ChangeRequestImageName { get; set; }//lc.status
    }

    public class TelecallerDashboardFeedBacksHistoryOutputApiModel
    {
        public string? Id { get; set; }
        public string? CustomerMet { get; set; }
        public string? DispositionCode { get; set; }
        public DateTime? PTPDate { get; set; }
        public decimal? PTPAmount { get; set; }
        public string? EscalateTo { get; set; }
        public string? IsReallocationRequest { get; set; }
        public string? ReallocationRequestReason { get; set; }
        public string? NewArea { get; set; }
        public string? NewAddress { get; set; }
        public string? City { get; set; }
        public string? NewContact { get; set; }
        public string? Remarks { get; set; }
        public string? CollectorFirstName { get; set; }
        public string? RightPartyContract { get; set; }
        public string? CollectorLastName { get; set; }
        public DateTime? DispositionDate { get; set; }
        public string? State { get; set; }
        public string? AssignToLastName { get; set; }
        public string? AssignToFirstName { get; set; }
        public string? DispositionGroup { get; set; }
        public string? PickAddress { get; set; }
        public string? OtherPickAddress { get; set; }
        public string? UploadedFileName { get; set; }
        public bool IsWerRTCHistoryExist { get; set; }
        public string? Place_of_visit { get; set; }
        public string? ThirdPartyContactPerson { get; set; }
        public string? NonPaymentReason { get; set; }
        public string? AgentContactNo { get; set; }
        public string? ModeOfCommunication { get; set; }
             
        public string? AssignReason { get; set; }
    }

    public class LoanAccountNotesOutputApiModel
    {
        public string? Id { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public string? UserName { get; set; }
        public string? UserCode { get; set; }
        public DateTime NoteDateTime { get; set; }
    }

    public class TelecallerAccountCommunicationsOutputModel
    {
        public string? Id { get; set; }
        public string? CustomId { get; set; }
        public string? Channel { get; set; }
        public string? Template { get; set; }
        public DateTime? ExecutionDate { get; set; }
        public string? PODNo { get; set; }
        public DateTime? DispatchDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string? Status { get; set; }
        public string? ReturnReason { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string? FileName { get; set; }
    }

    public class LoanAccountFlagsOutputApiModel
    {
        [StringLength(50)]
        public string? Name { get; set; }

        public bool IsActive { get; set; }

        [StringLength(32)]
        public string? Id { get; set; }

        public string? UserName { get; set; }
        public string? UserCode { get; set; }
        public DateTime FlagDateTime { get; set; }
    }

    public class ChequeDetailsOutputApiModel
    {
        public string? Id { get; set; }
        public string? INSTRMNT_ID { get; set; }
        public string? MICR_CODE { get; set; }
        public string? ISS_BANK_CODE { get; set; }
        public string? ISS_BR_CODE { get; set; }
        public decimal? INSTRMNT_AMT { get; set; }
        public DateTime? INSTRMNT_DATE { get; set; }
        public string? INSTRMNT_TYPE { get; set; }
        public string? REJECT_REASON { get; set; }
    }

    public class ENCollectLoanAccountCoApplicants
    {
        [StringLength(50)]
        public string? RELATION_TYPE { get; set; }

        [StringLength(50)]
        public string? CUST_RELTN_CODE { get; set; }

        [StringLength(9)]
        public string? CUST_ID { get; set; }

        [StringLength(80)]
        public string? CUST_NAME { get; set; }

        [StringLength(200)]
        public string? CUST_PERM_ADDR1 { get; set; }

        [StringLength(50)]
        public string? CITY_CODE { get; set; }

        [StringLength(50)]
        public string? STATE_CODE { get; set; }

        [StringLength(20)]
        public string? PHONE_NUM1 { get; set; }

        [StringLength(20)]
        public string? PHONE_NUM2 { get; set; }

        [StringLength(50)]
        public string? EMAIL_ID { get; set; }

        // public ENCollectLoanAccount ENCollectLoanAccount { get; set; }
        [StringLength(32)]
        public string? ENCollectLoanAccountId { get; set; }
    }

    public class ENCollectLoanAccountSecurities
    {
        [StringLength(240)]
        public string? SECURITY_TYPE { get; set; }

        [StringLength(240)]
        public string? SECURITY_NATURE { get; set; }

        public decimal? SECURITY_CURR_VALUE { get; set; }

        [StringLength(50)]
        public string? SECURITY_SNO { get; set; }

        //public ENCollectLoanAccount ENCollectLoanAccount { get; set; }
        [StringLength(32)]
        public string? ENCollectLoanAccountId { get; set; }
    }

    public class ENCollectLoanAccountDisbursements
    {
        public DateTime? DISB_DATE { get; set; }

        public decimal? DISB_AMT { get; set; }

        [StringLength(5)]
        public string? FLOW_ID { get; set; }

        public DateTime? TRAN_DATE { get; set; }

        [StringLength(9)]
        public string? TRAN_ID { get; set; }

        // public ENCollectLoanAccount ENCollectLoanAccount { get; set; }
        [StringLength(32)]
        public string? ENCollectLoanAccountId { get; set; }
    }

    public class ENCollectLoanAccountCharges
    {
        public decimal? OTH_CHARGES { get; set; }
        public DateTime? CHG_DEBIT_DATE { get; set; }

        //public ENCollectLoanAccount ENCollectLoanAccount { get; set; }
        [StringLength(32)]
        public string? ENCollectLoanAccountId { get; set; }
    }

    public class ENCollectLoanAccountsProjection
    {
        public DateTime? TRAN_DATE { get; set; }

        public DateTime? VALUE_DATE { get; set; }

        [StringLength(200)]
        public string? MODE_OF_PAYMENT { get; set; }

        public decimal? TRAN_AMT { get; set; }

        [StringLength(1)]
        public string? PART_TRAN_TYPE { get; set; }

        [StringLength(5)]
        public string? DMD_FLOW_ID { get; set; }

        [StringLength(200)]
        public string? TRAN_RMKS { get; set; }

        //public ENCollectLoanAccount ENCollectLoanAccount { get; set; }
        [StringLength(32)]
        public string? ENCollectLoanAccountId { get; set; }
    }

    public class ENCollectCreditBureauDetails
    {
        [StringLength(40)]
        public string? CUSTOMERID { get; set; }

        [StringLength(40)]
        public string? ACCOUNT_NB { get; set; }

        [StringLength(15)]
        public string? M_SUB_ID { get; set; }

        [StringLength(30)]
        public string? ACCT_TYPE_CD { get; set; }

        public DateTime? OPEN_DT { get; set; }
        public decimal? ACTUAL_PAYMENT_AM { get; set; }

        [StringLength(8)]
        public string? ASSET_CLASS_CD { get; set; }

        public decimal? BALANCE_AM { get; set; }
        public DateTime? BALANCE_DT { get; set; }
        public decimal? CHAREGE_OFF_AM { get; set; }
        public DateTime CLOSED_DT { get; set; }
        public decimal? CREDIT_LIMIT_AM { get; set; }
        public decimal? DAY_PAST_DUE { get; set; }
        public DateTime? DFLT_STATUS_DT { get; set; }
        public DateTime? LAST_PAYMENT_DT { get; set; }
        public decimal? ORIG_LOAN_AM { get; set; }
        public decimal? PAST_DUE_AM { get; set; }

        [StringLength(40)]
        public string? PAYMENT_HISTORY_GRID { get; set; }

        [StringLength(15)]
        public string? SUIT_FILED_WILLFUL_DFLT { get; set; }

        [StringLength(15)]
        public string? WRITTEN_OFF_AND_SETTLED_STATUS { get; set; }

        public DateTime? WRITE_OFF_STATUS_DT { get; set; }
        public decimal CIBILSCORE { get; set; }

        [StringLength(500)]
        public string? CONTACTADDRESS { get; set; }

        public string? CONTACTNOS { get; set; }

        [StringLength(40)]
        public string? DELSTRING { get; set; }
    }

    public class AccountLatestContactDetailsDto
    {
        public string? Latest_Number_From_Trail { get; set; }
        public string? Latest_Email_From_Trail { get; set; }
        public string? Latest_Address_From_Trail { get; set; }
        public string? Latest_Number_From_Receipt { get; set; }
        public string? Latest_Number_From_Send_Payment { get; set; }
    }
}