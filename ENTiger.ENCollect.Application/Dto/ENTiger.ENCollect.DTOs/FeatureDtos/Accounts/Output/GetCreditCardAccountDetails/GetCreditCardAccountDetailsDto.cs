namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetCreditCardAccountDetailsDto : DtoBridge
    {
        public CreditCardAccountOutputApiModel Account { get; set; }
        public AllocationHistoryOutputApiModel CollectionPoint { get; set; }
        public ICollection<DashboardCollectionsOutputApiModel> CollectionHistory { get; set; }
        public ICollection<DashboardFeedBacksHistoryOutputApiModel> FeedBackHistory { get; set; }
        public ICollection<AccountCommunicationsOutputModel> Communications { get; set; }
        public ICollection<LoanAccountNotesOutputApiModel> Notes { get; set; }
    }

    public class CreditCardAccountOutputApiModel
    {
        public string? AccountJSON { get; set; }
        public CCCaseDetailsOutputApiModel CaseDetails { get; set; }
        public CCAnalyticsOutputAPIModel Analytics { get; set; }
        public CCFinancialsOutputApiModel CardFinancials { get; set; }
        public CCStatusOutputApiModel CaseStatus { get; set; }
        public CCDemogDetailsOutputApiModel DemographicDetails { get; set; }
        public CCAdditionalDetailsOutputAPIModel AdditionalDetails { get; set; }
        public CCOtherContactDetailsOutputAPIModel OtherContactDetails { get; set; }
        public CCOtherDetailsOutputAPIModel OtherDetails { get; set; }
        public CCPaymentAndBounceOutputAPIModel PaymentAndBounceDetails { get; set; }
        public AccountLatestContactDetailsDto ContactDetails { get; set; }
    }

    public class CCCaseDetailsOutputApiModel
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
        public string? LAST_PAYMENT_DATE { get; set; }
        public string? DueDate { get; set; }
        public string? WRITEOFFDATE { get; set; }
        public string? ECS_ENABLED { get; set; }
        public string? BILLING_CYCLE { get; set; }
        public string? CARD_OPEN_DATE { get; set; }
        public string? CASH_LIMIT { get; set; }
        public string? COLLECTIONS_CREATION_DATE { get; set; }
        public string? CREDIT_LIMIT { get; set; }
        public string? CURRENT_BALANCE_AMOUNT { get; set; }
        public string? CURRENT_BLOCK_CODE { get; set; }
        public string? CURRENT_BLOCK_CODE_DATE { get; set; }
        public string? LAST_PAYMENT_MODE { get; set; }
        public string? LAST_PAYMENT_REFERENCE_NUMBER { get; set; }
        public string? LAST_PAYMENT_REMARK { get; set; }
        public string? LOGO { get; set; }
        public string? LOGO_DESCRIPTION { get; set; }
        public string? PRIMARY_CARD_NUMBER { get; set; }
        public string? STATEMENTED_BLOCK_CODE { get; set; }
        public string? STATEMENTED_DUE_DATE_PRINTED { get; set; }
        public string? STATEMENTED_DUE_DATE_SYSTEM { get; set; }
        public string? STATEMENTED_OPENING_BALANCE { get; set; }
        public bool? IsEligibleForSettlement { get; set; }
        public bool? IsEligibleForLegal { get; set; }
        public bool? IsEligibleForRepossession { get; set; }
        public bool? IsEligibleForRestructure { get; set; }
        public string? RephasementFlag { get; set; }
    }

    public class CCAnalyticsOutputAPIModel
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

    public class CCFinancialsOutputApiModel
    {
        public string? LAST_PAYMENT_AMOUNT { get; set; }
        public string? CURRENT_DAYS_PAYMENT_DUE { get; set; }
        public decimal? CURRENT_MINIMUM_AMOUNT_DUE { get; set; }
        public decimal? CURRENT_TOTAL_AMOUNT_DUE { get; set; }
        public string? OVERLIMIT_AMOUNT { get; set; }
        public string? PAST_DAYS_PAYMENT_DUE { get; set; }
        public string? PAYMENT_CYCLE_DUE { get; set; }
        public string? PAYMENT_DUE_120_DAYS { get; set; }
        public string? PAYMENT_DUE_150_DAYS { get; set; }
        public string? PAYMENT_DUE_180_DAYS { get; set; }
        public string? PAYMENT_DUE_210_DAYS { get; set; }
        public string? PAYMENT_DUE_30_DAYS { get; set; }
        public string? PAYMENT_DUE_60_DAYS { get; set; }
        public string? PAYMENT_DUE_90_DAYS { get; set; }
        public string? WRITE_OFF_AMOUNT { get; set; }
    }

    public class CCStatusOutputApiModel
    {
        public string? OVERDUE_DAYS { get; set; }
        public long? CURRENT_DPD { get; set; }
        public string? CURRENT_BUCKET { get; set; }
        public long? BUCKET { get; set; }
        public string? NPA_STAGEID { get; set; }
        public string? PAYMENTSTATUS { get; set; }
        public string? Month_on_Books { get; set; }
        public string? NO_OF_EMI_OD { get; set; }
    }

    public class CCDemogDetailsOutputApiModel
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
        public string? CUSTOMERS_PERMANENT_EMAIL_ID { get; set; }
        public string? NewAddress { get; set; }
        public string? LatestMobileNo { get; set; }
    }

    public class CCAdditionalDetailsOutputAPIModel
    {
        public string? LAST_CHEQUE_BOUNCE_AMOUNT { get; set; }
        public string? LAST_CHEQUE_BOUNCE_DATE { get; set; }
        public string? LAST_CHEQUE_BOUNCE_NUMBER { get; set; }
        public string? LAST_CHEQUE_BOUNCE_REASON { get; set; }
        public string? LAST_CHEQUE_ISSUANCE_BANK_NAME { get; set; }
        public string? LAST_PURCHASED_AMOUNT { get; set; }
        public string? LAST_PURCHASED_DATE { get; set; }
        public string? LAST_PURCHASED_TRANSACTION_REMARK { get; set; }
        public string? TOTAL_PURCHASES { get; set; }
    }

    public class CCOtherDetailsOutputAPIModel
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

    public class CCOtherContactDetailsOutputAPIModel
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

    public class CCPaymentAndBounceOutputAPIModel
    {
        public DateTime? LAST_STATEMENT_DATE { get; set; }
        public string? LAST_STATEMENT_SENT_AT_ADDRESS_OR_NOT_FLAG { get; set; }
        public string? LAST_STATEMENT_SENT_AT_ADDRESS_TYPE { get; set; }
    }

    public class AllocationHistoryOutputApiModel
    {
        public string? SourcingAgentName { get; set; }
        public string? AllocationMethod { get; set; }
        public string? AllocationType { get; set; }
        public DateTime? TransactionDate { get; set; }
        public DateTime? AllocationExpiryDate { get; set; }
        public string? TransactionId { get; set; }
        public string? AllocationOwnerCode { get; set; }
        public string? AllocationOwnerName { get; set; }
        public string? AllocationOwnerDesignation { get; set; }
        public string? TelecallingAgencyCode { get; set; }
        public string? TelecallingAgencyName { get; set; }
        public string? TelecallerCode { get; set; }
        public string? TelecallerName { get; set; }
        public string? AgencyCode { get; set; }
        public string? AgencyName { get; set; }
        public string? AgentCode { get; set; }
        public string? AgentName { get; set; }
    }

    public class DashboardCollectionsOutputApiModel
    {
        public string Id { get; set; }//colection.paymentdate
        public string ReceiptNo { get; set; }
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

    public class DashboardFeedBacksHistoryOutputApiModel
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
        public string? AssignReason { get; set; }
    }

    public class AccountCommunicationsOutputModel
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
}