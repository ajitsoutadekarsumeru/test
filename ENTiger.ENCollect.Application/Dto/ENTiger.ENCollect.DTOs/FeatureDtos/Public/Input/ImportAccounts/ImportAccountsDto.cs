using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.PublicModule
{
    public partial class ImportAccountsDto : DtoBridge
    {
        [Required]
        public string? entity { get; set; }

        [Required]
        public int count { get; set; }

        public List<AccountImportDto> items { get; set; }

        public string? CustomId { get; set; }
    }

    public class AccountImportDto : DtoBridge
    {
        [Required]
        public string? AGREEMENTID { get; set; }

        public string? LenderId { get; set; }
        public string? Partner_Loan_ID { get; set; }
        public string? Partner_Customer_ID { get; set; }
        public string? Partner_Name { get; set; }
        public string? CUSTOMERID { get; set; }
        public string? CUSTOMERNAME { get; set; }
        public string? DateOfBirth { get; set; }//DateTime Changed to sring because of datetime format issue
        public string? GENDER { get; set; }
        public string? EMAIL_ID { get; set; }
        public string? PAN_CARD_DETAILS { get; set; }
        public string? MaritalStatus { get; set; }
        public string? SZ_SPOUSE_NAME { get; set; }
        public string? NameOfBusiness { get; set; }
        public string? RegistrationNo { get; set; }
        public string? TAXIdentificationNumber { get; set; }
        public string? Area { get; set; }
        public string? CustomerPersona { get; set; }
        public string? ProductGroup { get; set; }
        public string? ProductCode { get; set; }
        public string? PRODUCT { get; set; }
        public string? SubProduct { get; set; }
        public string? COMMUNICATION_COUNTRY_CODE { get; set; }
        public string? PERMANENT_COUNTRY_CODE { get; set; }
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
        public string? PinCode { get; set; }
        public string? MAILINGPHONE1 { get; set; }
        public string? MAILINGPHONE2 { get; set; }
        public string? MAILINGMOBILE { get; set; }
        public string? NONMAILINGADDRESS { get; set; }
        public string? ADDRESSTYPE1 { get; set; }
        public string? ADDRESSTYPE2 { get; set; }
        public string? FATHERS_CONTACTS { get; set; }
        public string? EMPLOYER_CITY_CODE { get; set; }
        public string? EMPLOYER_STATE_CODE { get; set; }
        public string? GUARANTOR_1 { get; set; }
        public string? GUARANTOR_2 { get; set; }
        public string? GUARANTOR_3 { get; set; }
        public string? GUARANTOR_4 { get; set; }
        public string? GUARANTOR1_CITY { get; set; }
        public string? GUARANTOR1_COUNTY { get; set; }
        public string? CO_APPLICANT1_NAME { get; set; }
        public string? CO_APPLICANT1_CONTACT { get; set; }
        public string? CO_APPLICANT_1 { get; set; }
        public string? CO_APPLICANT_2 { get; set; }
        public string? CO_APPLICANT_3 { get; set; }
        public string? CO_APPLICANT_4 { get; set; }
        public string? FATHERS_ADDRS { get; set; }
        public string? FATHERNAME { get; set; }
        public string? MOTHERS_ADDRS { get; set; }
        public string? MOTHERS_CONTACTS { get; set; }
        public string? MOTHERS_NAME { get; set; }
        public string? EMPLOYER_ADDRESS { get; set; }
        public string? ADD_ON_ADDRESS_1 { get; set; }
        public string? REF1_CONTACT { get; set; }
        public string? REFERENCE1_NAME { get; set; }
        public string? REFERENCE1_ADDRS { get; set; }
        public string? REF1_ADDRS { get; set; }
        public string? REFERENCE2_NAME { get; set; }
        public string? REF2_ADDRS { get; set; }
        public string? NONMAILINGPHONE1 { get; set; }
        public string? NONMAILINGPHONE2 { get; set; }
        public string? REF2_CONTACT { get; set; }
        public string? PHYSICAL_ADDRESS { get; set; }
        public string? NONMAILINGCITY { get; set; }
        public string? NONMAILINGLANDMARK { get; set; }
        public string? NONMAILINGMOBILE { get; set; }
        public string? NONMAILINGSTATE { get; set; }
        public string? NONMAILINGZIPCODE { get; set; }
        public string? OVERDUE_DAYS { get; set; }
        public int? CURRENT_DPD { get; set; }
        public string? CURRENT_BUCKET { get; set; }
        public int BUCKET { get; set; }
        public string? NPA_STAGEID { get; set; }
        public string? PAYMENTSTATUS { get; set; }
        public int? Month_on_Books { get; set; }
        public string? NO_OF_EMI_OD { get; set; }
        public string? LoanPeriodInDays { get; set; }
        public string? TENURE_MONTH { get; set; }
        public int? Tenure_Days { get; set; }
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
        public string? LOAN_STATUS { get; set; }
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
        public string? BANK_ACC_NUM { get; set; }
        public string? BANK_CODE { get; set; }
        public string? BANK_NAME { get; set; }
        public string? BCC_PENDING { get; set; }
        public string? SanctionDate { get; set; }//DateTime Changed to sring because of datetime format issue
        public string? LOAN_DATE { get; set; }
        public string? DISBURSALDATE { get; set; }
        public string? NEXT_DUE_DATE { get; set; }
        public string? TOTAL_PAID_TO_DATE { get; set; }
        public string? LAST_PAYMENT_DATE { get; set; }//DateTime Changed to sring because of datetime format issue
        public string? PTPDate { get; set; }//DateTime Changed to sring because of datetime format issue
        public string? DueDate { get; set; }
        public string? REPAYMENT_STARTDATE { get; set; }//DateTime Changed to sring because of datetime format issue
        public string? WRITEOFFDATE { get; set; }//DateTime Changed to sring because of datetime format issue
        public string? EMI_START_DATE { get; set; }
        public string? CHARGEOFF_DATE { get; set; }
        public string? OVERDUE_DATE { get; set; }
        public decimal? ASSET_LOAN_AMOUNT { get; set; }
        public decimal? EMIAMT { get; set; }
        public string? PRINCIPAL_OUTSTANDING { get; set; }
        public decimal? TOTAL_INTEREST { get; set; }
        public decimal? INTEREST_OD { get; set; }
        public decimal? INTEREST_AMOUNT { get; set; }
        public string? PENAL_ST { get; set; }
        public decimal? OtherchargesOverdue { get; set; }
        public decimal? PENAL_PENDING { get; set; }
        public decimal? LOAN_LIABILITY { get; set; }
        public decimal? EMI_OD_AMT { get; set; }
        public decimal? CURRENT_POS { get; set; }
        public string? NEXT_DUE_AMOUNT { get; set; }
        public string? BOUNCED_CHEQUE_CHARGES { get; set; }
        public decimal? LAST_PAYMENT_AMOUNT { get; set; }
        public decimal? PRINCIPAL_OD { get; set; }
        public decimal? BOM_POS { get; set; }
        public string? SACT_AMOUNT { get; set; }
        public string? DISBURSEDAMOUNT { get; set; }
        public string? LOAN_AMOUNT { get; set; }
        public decimal? OTHER_CHARGES { get; set; }
        public string? ForeclosePrepayCharge { get; set; }
        public string? TOS { get; set; }
        public string? TOTAL_ARREARS { get; set; }
        public string? OtherReceivables { get; set; }
        public string? Excess { get; set; }
        public decimal? TOTAL_OUTSTANDING { get; set; }
        public decimal? TOTAL_POS { get; set; }
        public decimal? TOTAL_BOUNCE_CHARGE { get; set; }
        public decimal? TOTAL_LATE_PAYMENT_CHARGE { get; set; }
        public decimal? TOTAL_INSURANCE_CHARGE { get; set; }
        public decimal? TOTAL_PROCESSING_CHARGE { get; set; }
        public decimal? TOTAL_VALUATION_CHARGE { get; set; }
        public decimal? OTHER_CHARGE { get; set; }
        public decimal? INT_AMOUNT { get; set; }
        public decimal? TOTAL_OVERDUE_AMT { get; set; }
        public string? MODE_OF_OPERATION { get; set; }
        public string? LOCATION_CODE { get; set; }
        public string? SCHEME_DESC { get; set; }
        public string? USER_CLASSIFICATION_DATE { get; set; }//DateTime Changed to sring because of datetime format issue
        public string? MAIN_CLASSIFICATION_USER { get; set; }
        public decimal? LEGAL_CHARGE { get; set; }
        public string? PAYMENT_TYPE { get; set; }
        public string? ECS_ENABLED { get; set; }
        public decimal? BUILDING_VALUATION { get; set; }
        public string? VALUATION_DATE { get; set; }//DateTime Changed to sring because of datetime format issue
        public string? DELstring { get; set; }
        public string? ADD_ON_CARD_NUMBER { get; set; }
        public string? ADD_ON_DESIGNATION { get; set; }
        public string? ADD_ON_FIRST_NAME { get; set; }
        public string? ADD_ON_LAST_NAME { get; set; }
        public string? ADD_ON_PERMANENT_CITY { get; set; }
        public string? ADD_ON_PERMANENT_COUNTRY { get; set; }
        public string? ADD_ON_PERMANENT_POST_CODE { get; set; }
        public string? ADD_ON_PERMANENT_STATE { get; set; }
        public string? ADD_ON_PERSONAL_EMAIL_ID { get; set; }
        public string? ADD_ON_WORK_EMAIL_ID { get; set; }
        public string? AD_MANDATE_SB_ACC_NUMBER { get; set; }
        public string? BILLING_CYCLE { get; set; }
        public string? CARD_OPEN_DATE { get; set; }//DateTime Changed to sring because of datetime format issue
        public decimal? CASH_LIMIT { get; set; }
        public string? COLLECTIONS_CREATION_DATE { get; set; }//DateTime Changed to sring because of datetime format issue
        public decimal? CREDIT_LIMIT { get; set; }
        public decimal? CURRENT_BALANCE_AMOUNT { get; set; }
        public string? CURRENT_BLOCK_CODE { get; set; }
        public string? CURRENT_BLOCK_CODE_DATE { get; set; }//DateTime Changed to sring because of datetime format issue
        public decimal? CURRENT_DAYS_PAYMENT_DUE { get; set; }
        public decimal? CURRENT_MINIMUM_AMOUNT_DUE { get; set; }
        public decimal? CURRENT_TOTAL_AMOUNT_DUE { get; set; }
        public string? CUSTOMERS_PERMANENT_EMAIL_ID { get; set; }
        public string? CUSTOMER_EMPLOYER { get; set; }
        public string? CUSTOMER_OFFICE_COUNTRY { get; set; }
        public string? CUSTOMER_OFFICE_PIN_CODE { get; set; }
        public decimal? LAST_CHEQUE_BOUNCE_AMOUNT { get; set; }
        public string? LAST_CHEQUE_BOUNCE_DATE { get; set; }//DateTime Changed to sring because of datetime format issue
        public string? LAST_CHEQUE_BOUNCE_NUMBER { get; set; }
        public string? LAST_CHEQUE_BOUNCE_REASON { get; set; }
        public string? LAST_CHEQUE_ISSUANCE_BANK_NAME { get; set; }
        public string? LAST_PAYMENT_MODE { get; set; }
        public string? LAST_PAYMENT_REFERENCE_NUMBER { get; set; }
        public string? LAST_PAYMENT_REMARK { get; set; }
        public decimal? LAST_PURCHASED_AMOUNT { get; set; }
        public string? LAST_PURCHASED_DATE { get; set; }//DateTime Changed to sring because of datetime format issue
        public string? LAST_PURCHASED_TRANSACTION_REMARK { get; set; }
        public string? LAST_STATEMENT_DATE { get; set; }//DateTime Changed to sring because of datetime format issue
        public string? LAST_STATEMENT_SENT_AT_ADDRESS_OR_NOT_FLAG { get; set; }
        public string? LAST_STATEMENT_SENT_AT_ADDRESS_TYPE { get; set; }
        public string? LOGO { get; set; }
        public string? LOGO_DESCRIPTION { get; set; }
        public decimal? OVERLIMIT_AMOUNT { get; set; }
        public decimal? PAST_DAYS_PAYMENT_DUE { get; set; }
        public decimal? PAYMENT_CYCLE_DUE { get; set; }
        public decimal? PAYMENT_DUE_120_DAYS { get; set; }
        public decimal? PAYMENT_DUE_150_DAYS { get; set; }
        public decimal? PAYMENT_DUE_180_DAYS { get; set; }
        public decimal? PAYMENT_DUE_210_DAYS { get; set; }
        public decimal? PAYMENT_DUE_30_DAYS { get; set; }
        public decimal? PAYMENT_DUE_60_DAYS { get; set; }
        public decimal? PAYMENT_DUE_90_DAYS { get; set; }
        public string? PRIMARY_CARD_NUMBER { get; set; }
        public string? RESIDENTIAL_COUNTRY { get; set; }
        public string? RESIDENTIAL_CUSTOMER_CITY { get; set; }
        public string? RESIDENTIAL_CUSTOMER_STATE { get; set; }
        public string? RESIDENTIAL_PIN_CODE { get; set; }
        public string? STATEMENTED_BLOCK_CODE { get; set; }
        public string? STATEMENTED_DUE_DATE_PRINTED { get; set; }//DateTime Changed to sring because of datetime format issue
        public string? STATEMENTED_DUE_DATE_SYSTEM { get; set; }//DateTime Changed to sring because of datetime format issue
        public decimal? STATEMENTED_OPENING_BALANCE { get; set; }
        public decimal? TOTAL_PURCHASES { get; set; }
        public decimal? WRITE_OFF_AMOUNT { get; set; }
        public string? PropertyVerificationTag { get; set; }
        public bool PropensityToPay { get; set; }
        public decimal? PropensityToPayConfidence { get; set; }
        public bool IsDNDEnabled { get; set; }
        public string? Name { get; set; }
        public string? DispCode { get; set; }
        public string? SegmentationCode { get; set; }
        public bool IsEligibleForSettlement { get; set; }
        public bool IsEligibleForLegal { get; set; }
        public bool IsEligibleForRepossession { get; set; }
        public bool IsEligibleForRestructure { get; set; }
        public bool? RephasementFlag { get; set; }
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
        public string? NewAddress { get; set; }
        public string? LatestMobileNo { get; set; }
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
}