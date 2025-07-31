using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class LoanAccountJsonEnum : FlexEnum
    {
        public LoanAccountJsonEnum()
        { }

        public LoanAccountJsonEnum(string value, string displayName) : base(value, displayName)
        {
        }

        public static readonly LoanAccountJsonEnum PartnerCustomerID = new LoanAccountJsonEnum("Partner_Customer_ID", "Partner_Customer_ID");

       
        public static readonly LoanAccountJsonEnum Status = new LoanAccountJsonEnum("Status", "Account Status");
        public static readonly LoanAccountJsonEnum PaymentDueDate = new LoanAccountJsonEnum("Paymentduedate", "Payment Due Date");
        public static readonly LoanAccountJsonEnum MAD = new LoanAccountJsonEnum("MAD", "MAD");
        public static readonly LoanAccountJsonEnum D180 = new LoanAccountJsonEnum("d180", "180 Days");
        public static readonly LoanAccountJsonEnum D150 = new LoanAccountJsonEnum("d150", "150 Days");
        public static readonly LoanAccountJsonEnum D120 = new LoanAccountJsonEnum("d120", "120 Days");
        public static readonly LoanAccountJsonEnum D90 = new LoanAccountJsonEnum("d90", "90 Days");
        public static readonly LoanAccountJsonEnum D60 = new LoanAccountJsonEnum("d60", "60 Days");
        public static readonly LoanAccountJsonEnum D30 = new LoanAccountJsonEnum("d30", "30 Days");
        public static readonly LoanAccountJsonEnum CurrentBalance = new LoanAccountJsonEnum("CurrentBalance", "Current Balance");

        
        public static readonly LoanAccountJsonEnum PropensityToPayConfidence = new LoanAccountJsonEnum("PropensityToPayConfidence", "Propensity to Pay Confidence");
        public static readonly LoanAccountJsonEnum PropensityToPay = new LoanAccountJsonEnum("PropensityToPay", "Propensity to Pay");
        public static readonly LoanAccountJsonEnum BCCPending = new LoanAccountJsonEnum("BCC_PENDING", "BCC Pending");
        public static readonly LoanAccountJsonEnum GroupName = new LoanAccountJsonEnum("GroupName", "Group Name");
        public static readonly LoanAccountJsonEnum Reference1Address = new LoanAccountJsonEnum("REFERENCE1_ADDRS", "Reference 1 Address");
        public static readonly LoanAccountJsonEnum PhysicalAddress = new LoanAccountJsonEnum("PHYSICAL_ADDRESS", "Physical Address");
        public static readonly LoanAccountJsonEnum MothersAddress = new LoanAccountJsonEnum("MOTHERS_ADDRS", "Mother's Address");
        public static readonly LoanAccountJsonEnum FathersAddress = new LoanAccountJsonEnum("FATHERS_ADDRS", "Father's Address");
        public static readonly LoanAccountJsonEnum Guarantor1City = new LoanAccountJsonEnum("GUARANTOR1_CITY", "Guarantor 1 City");
        public static readonly LoanAccountJsonEnum NewAddress = new LoanAccountJsonEnum("NewAddress", "New Address");
        public static readonly LoanAccountJsonEnum BouncedChequeCharges = new LoanAccountJsonEnum("BOUNCED_CHEQUE_CHARGES", "Bounced Cheque Charges");
        public static readonly LoanAccountJsonEnum PartnerName = new LoanAccountJsonEnum("Partner_Name", "Partner Name");
        public static readonly LoanAccountJsonEnum TotalBounceCharge = new LoanAccountJsonEnum("TOTAL_BOUNCE_CHARGE", "Total Bounce Charge");
        public static readonly LoanAccountJsonEnum TotalOverdueAmount = new LoanAccountJsonEnum("TOTAL_OVERDUE_AMT", "Total Overdue Amount");
        public static readonly LoanAccountJsonEnum TotalLatePaymentCharge = new LoanAccountJsonEnum("TOTAL_LATE_PAYMENT_CHARGE", "Total Late Payment Charge");
        public static readonly LoanAccountJsonEnum AssetType = new LoanAccountJsonEnum("AssetType", "Asset Type");
        public static readonly LoanAccountJsonEnum AssetDetails = new LoanAccountJsonEnum("AssetDetails", "Asset Details");
        public static readonly LoanAccountJsonEnum AssetName = new LoanAccountJsonEnum("AssetName", "AssetName");

        
        public static readonly LoanAccountJsonEnum LastPaymentDate = new LoanAccountJsonEnum("LAST_PAYMENT_DATE", "Last Payment Date");
        public static readonly LoanAccountJsonEnum WriteOffDate = new LoanAccountJsonEnum("WRITEOFFDATE", "Write-Off Date");
        public static readonly LoanAccountJsonEnum ECSEnabled = new LoanAccountJsonEnum("ECS_ENABLED", "ECS Enabled");
        public static readonly LoanAccountJsonEnum CardOpenDate = new LoanAccountJsonEnum("CARD_OPEN_DATE", "Card Open Date");
        public static readonly LoanAccountJsonEnum CashLimit = new LoanAccountJsonEnum("CASH_LIMIT", "Cash Limit");
        public static readonly LoanAccountJsonEnum CollectionsCreationDate = new LoanAccountJsonEnum("COLLECTIONS_CREATION_DATE", "Collections Creation Date");
        public static readonly LoanAccountJsonEnum CreditLimit = new LoanAccountJsonEnum("CREDIT_LIMIT", "Credit Limit");
        public static readonly LoanAccountJsonEnum CurrentBalanceAmount = new LoanAccountJsonEnum("CURRENT_BALANCE_AMOUNT", "Current Balance Amount");
        public static readonly LoanAccountJsonEnum CurrentBlockCode = new LoanAccountJsonEnum("CURRENT_BLOCK_CODE", "Current Block Code");
        public static readonly LoanAccountJsonEnum CurrentBlockCodeDate = new LoanAccountJsonEnum("CURRENT_BLOCK_CODE_DATE", "Current Block Code Date");
        public static readonly LoanAccountJsonEnum LastPaymentMode = new LoanAccountJsonEnum("LAST_PAYMENT_MODE", "Last Payment Mode");
        public static readonly LoanAccountJsonEnum LastPaymentReferenceNumber = new LoanAccountJsonEnum("LAST_PAYMENT_REFERENCE_NUMBER", "Last Payment Reference Number");
        public static readonly LoanAccountJsonEnum LastPaymentRemark = new LoanAccountJsonEnum("LAST_PAYMENT_REMARK", "Last Payment Remark");
        public static readonly LoanAccountJsonEnum Logo = new LoanAccountJsonEnum("LOGO", "Logo");
        public static readonly LoanAccountJsonEnum LogoDescription = new LoanAccountJsonEnum("LOGO_DESCRIPTION", "Logo Description");
        public static readonly LoanAccountJsonEnum StatementedBlockCode = new LoanAccountJsonEnum("STATEMENTED_BLOCK_CODE", "Statemented Block Code");
        public static readonly LoanAccountJsonEnum StatementedDueDatePrinted = new LoanAccountJsonEnum("STATEMENTED_DUE_DATE_PRINTED", "Statemented Due Date Printed");
        public static readonly LoanAccountJsonEnum StatementedDueDateSystem = new LoanAccountJsonEnum("STATEMENTED_DUE_DATE_SYSTEM", "Statemented Due Date System");
        public static readonly LoanAccountJsonEnum StatementedOpeningBalance = new LoanAccountJsonEnum("STATEMENTED_OPENING_BALANCE", "Statemented Opening Balance");
        public static readonly LoanAccountJsonEnum RephasementFlag = new LoanAccountJsonEnum("RephasementFlag", "Rephasement Flag");

       
        public static readonly LoanAccountJsonEnum DelString = new LoanAccountJsonEnum("DELSTRING", "Del String");
        public static readonly LoanAccountJsonEnum SegmentationCode = new LoanAccountJsonEnum("SegmentationCode", "Segmentation Code");
        public static readonly LoanAccountJsonEnum PAR = new LoanAccountJsonEnum("PAR", "PAR");

       
        public static readonly LoanAccountJsonEnum LastPaymentAmount = new LoanAccountJsonEnum("LAST_PAYMENT_AMOUNT", "Last Payment Amount");
        public static readonly LoanAccountJsonEnum CurrentDaysPaymentDue = new LoanAccountJsonEnum("CURRENT_DAYS_PAYMENT_DUE", "Current Days Payment Due");
        public static readonly LoanAccountJsonEnum OverlimitAmount = new LoanAccountJsonEnum("OVERLIMIT_AMOUNT", "Overlimit Amount");
        public static readonly LoanAccountJsonEnum PastDaysPaymentDue = new LoanAccountJsonEnum("PAST_DAYS_PAYMENT_DUE", "Past Days Payment Due");
        public static readonly LoanAccountJsonEnum PaymentCycleDue = new LoanAccountJsonEnum("PAYMENT_CYCLE_DUE", "Payment Cycle Due");
        public static readonly LoanAccountJsonEnum PaymentDue120Days = new LoanAccountJsonEnum("PAYMENT_DUE_120_DAYS", "Payment Due 120 Days");
        public static readonly LoanAccountJsonEnum PaymentDue150Days = new LoanAccountJsonEnum("PAYMENT_DUE_150_DAYS", "Payment Due 150 Days");
        public static readonly LoanAccountJsonEnum PaymentDue180Days = new LoanAccountJsonEnum("PAYMENT_DUE_180_DAYS", "Payment Due 180 Days");
        public static readonly LoanAccountJsonEnum PaymentDue210Days = new LoanAccountJsonEnum("PAYMENT_DUE_210_DAYS", "Payment Due 210 Days");
        public static readonly LoanAccountJsonEnum PaymentDue30Days = new LoanAccountJsonEnum("PAYMENT_DUE_30_DAYS", "Payment Due 30 Days");
        public static readonly LoanAccountJsonEnum PaymentDue60Days = new LoanAccountJsonEnum("PAYMENT_DUE_60_DAYS", "Payment Due 60 Days");
        public static readonly LoanAccountJsonEnum PaymentDue90Days = new LoanAccountJsonEnum("PAYMENT_DUE_90_DAYS", "Payment Due 90 Days");
        public static readonly LoanAccountJsonEnum WriteOffAmount = new LoanAccountJsonEnum("WRITE_OFF_AMOUNT", "Write-Off Amount");

        public static readonly LoanAccountJsonEnum MonthOnBooks = new LoanAccountJsonEnum("Month_on_Books", "Month on Books");
        public static readonly LoanAccountJsonEnum Gender = new LoanAccountJsonEnum("GENDER", "Gender");
        public static readonly LoanAccountJsonEnum MaritalStatus = new LoanAccountJsonEnum("MaritalStatus", "Marital Status");
        public static readonly LoanAccountJsonEnum SZSpouseName = new LoanAccountJsonEnum("SZ_SPOUSE_NAME", "Spouse Name");
        public static readonly LoanAccountJsonEnum NameOfBusiness = new LoanAccountJsonEnum("NameOfBusiness", "Name of Business");
        public static readonly LoanAccountJsonEnum RegistrationNo = new LoanAccountJsonEnum("RegistrationNo", "Registration Number");
        public static readonly LoanAccountJsonEnum TAXIdentificationNumber = new LoanAccountJsonEnum("TAXIdentificationNumber", "Tax Identification Number");
        public static readonly LoanAccountJsonEnum CommunicationCountryCode = new LoanAccountJsonEnum("COMMUNICATION_COUNTRY_CODE", "Communication Country Code");
        public static readonly LoanAccountJsonEnum CityTier = new LoanAccountJsonEnum("CityTier", "City Tier");
        public static readonly LoanAccountJsonEnum BranchCodeKey = new LoanAccountJsonEnum("BRANCH_CODE", "Branch Code");
        public static readonly LoanAccountJsonEnum MailingAddress = new LoanAccountJsonEnum("MAILINGADDRESS", "Mailing Address");
        public static readonly LoanAccountJsonEnum MailingPhone1 = new LoanAccountJsonEnum("MAILINGPHONE1", "Mailing Phone 1");
        public static readonly LoanAccountJsonEnum MailingPhone2 = new LoanAccountJsonEnum("MAILINGPHONE2", "Mailing Phone 2");
        public static readonly LoanAccountJsonEnum AddressType1 = new LoanAccountJsonEnum("ADDRESSTYPE1", "Address Type 1");
        public static readonly LoanAccountJsonEnum FathersContacts = new LoanAccountJsonEnum("FATHERS_CONTACTS", "Father's Contacts");
        public static readonly LoanAccountJsonEnum Guarantor1 = new LoanAccountJsonEnum("GUARANTOR_1", "Guarantor 1");
        public static readonly LoanAccountJsonEnum Guarantor2 = new LoanAccountJsonEnum("GUARANTOR_2", "Guarantor 2");
        public static readonly LoanAccountJsonEnum Guarantor3 = new LoanAccountJsonEnum("GUARANTOR_3", "Guarantor 3");
        public static readonly LoanAccountJsonEnum Guarantor4 = new LoanAccountJsonEnum("GUARANTOR_4", "Guarantor 4");
        public static readonly LoanAccountJsonEnum CoApplicant1Contact = new LoanAccountJsonEnum("CO_APPLICANT1_CONTACT", "Co-Applicant 1 Contact");
        public static readonly LoanAccountJsonEnum CoApplicant1 = new LoanAccountJsonEnum("CO_APPLICANT_1", "Co-Applicant 1");
        public static readonly LoanAccountJsonEnum CoApplicant2 = new LoanAccountJsonEnum("CO_APPLICANT_2", "Co-Applicant 2");
        public static readonly LoanAccountJsonEnum CoApplicant3 = new LoanAccountJsonEnum("CO_APPLICANT_3", "Co-Applicant 3");
        public static readonly LoanAccountJsonEnum CoApplicant4 = new LoanAccountJsonEnum("CO_APPLICANT_4", "Co-Applicant 4");
        public static readonly LoanAccountJsonEnum FatherName = new LoanAccountJsonEnum("FATHERNAME", "Father's Name");
        public static readonly LoanAccountJsonEnum MothersContacts = new LoanAccountJsonEnum("MOTHERS_CONTACTS", "Mother's Contacts");
        public static readonly LoanAccountJsonEnum MothersName = new LoanAccountJsonEnum("MOTHERS_NAME", "Mother's Name");
        public static readonly LoanAccountJsonEnum Ref1Contact = new LoanAccountJsonEnum("REF1_CONTACT", "Reference 1 Contact");
        public static readonly LoanAccountJsonEnum Reference1Name = new LoanAccountJsonEnum("REFERENCE1_NAME", "Reference 1 Name");
        public static readonly LoanAccountJsonEnum Reference2Name = new LoanAccountJsonEnum("REFERENCE2_NAME", "Reference 2 Name");
        public static readonly LoanAccountJsonEnum NonMailingPhone1 = new LoanAccountJsonEnum("NONMAILINGPHONE1", "Non-Mailing Phone 1");
        public static readonly LoanAccountJsonEnum NonMailingPhone2 = new LoanAccountJsonEnum("NONMAILINGPHONE2", "Non-Mailing Phone 2");
        public static readonly LoanAccountJsonEnum Ref2Contact = new LoanAccountJsonEnum("REF2_CONTACT", "Reference 2 Contact");
        public static readonly LoanAccountJsonEnum NonMailingMobile = new LoanAccountJsonEnum("NONMAILINGMOBILE", "Non-Mailing Mobile");
        public static readonly LoanAccountJsonEnum AddOnCardNumber = new LoanAccountJsonEnum("ADD_ON_CARD_NUMBER", "Add-On Card Number");
        public static readonly LoanAccountJsonEnum AddOnDesignation = new LoanAccountJsonEnum("ADD_ON_DESIGNATION", "Add-On Designation");
        public static readonly LoanAccountJsonEnum AddOnFirstName = new LoanAccountJsonEnum("ADD_ON_FIRST_NAME", "Add-On First Name");
        public static readonly LoanAccountJsonEnum AddOnLastName = new LoanAccountJsonEnum("ADD_ON_LAST_NAME", "Add-On Last Name");
        public static readonly LoanAccountJsonEnum AddOnPermanentCity = new LoanAccountJsonEnum("ADD_ON_PERMANENT_CITY", "Add-On Permanent City");
        public static readonly LoanAccountJsonEnum AddOnPermanentCountry = new LoanAccountJsonEnum("ADD_ON_PERMANENT_COUNTRY", "Add-On Permanent Country");
        public static readonly LoanAccountJsonEnum AddOnPermanentPostCode = new LoanAccountJsonEnum("ADD_ON_PERMANENT_POST_CODE", "Add-On Permanent Post Code");
        public static readonly LoanAccountJsonEnum AddOnPermanentState = new LoanAccountJsonEnum("ADD_ON_PERMANENT_STATE", "Add-On Permanent State");
        public static readonly LoanAccountJsonEnum AddOnPersonalEmailId = new LoanAccountJsonEnum("ADD_ON_PERSONAL_EMAIL_ID", "Add-On Personal Email ID");
        public static readonly LoanAccountJsonEnum AddOnWorkEmailId = new LoanAccountJsonEnum("ADD_ON_WORK_EMAIL_ID", "Add-On Work Email ID");
        public static readonly LoanAccountJsonEnum AdMandateSbAccNumber = new LoanAccountJsonEnum("AD_MANDATE_SB_ACC_NUMBER", "AD Mandate SB Account Number");
        public static readonly LoanAccountJsonEnum CustomersPermanentEmailId = new LoanAccountJsonEnum("CUSTOMERS_PERMANENT_EMAIL_ID", "Customer's Permanent Email ID");

        public static readonly LoanAccountJsonEnum LastChequeBounceAmount = new LoanAccountJsonEnum("LAST_CHEQUE_BOUNCE_AMOUNT", "Last Cheque Bounce Amount");
        public static readonly LoanAccountJsonEnum LastChequeBounceDate = new LoanAccountJsonEnum("LAST_CHEQUE_BOUNCE_DATE", "Last Cheque Bounce Date");
        public static readonly LoanAccountJsonEnum LastChequeBounceNumber = new LoanAccountJsonEnum("LAST_CHEQUE_BOUNCE_NUMBER", "Last Cheque Bounce Number");
        public static readonly LoanAccountJsonEnum LastChequeBounceReason = new LoanAccountJsonEnum("LAST_CHEQUE_BOUNCE_REASON", "Last Cheque Bounce Reason");
        public static readonly LoanAccountJsonEnum LastChequeIssuanceBankName = new LoanAccountJsonEnum("LAST_CHEQUE_ISSUANCE_BANK_NAME", "Last Cheque Issuance Bank Name");
        public static readonly LoanAccountJsonEnum LastPurchasedAmount = new LoanAccountJsonEnum("LAST_PURCHASED_AMOUNT", "Last Purchased Amount");
        public static readonly LoanAccountJsonEnum LastPurchasedDate = new LoanAccountJsonEnum("LAST_PURCHASED_DATE", "Last Purchased Date");
        public static readonly LoanAccountJsonEnum LastPurchasedTransactionRemark = new LoanAccountJsonEnum("LAST_PURCHASED_TRANSACTION_REMARK", "Last Purchased Transaction Remark");
        public static readonly LoanAccountJsonEnum TotalPurchases = new LoanAccountJsonEnum("TOTAL_PURCHASES", "Total Purchases");

        public static readonly LoanAccountJsonEnum PermanentCountryCode = new LoanAccountJsonEnum("PERMANENT_COUNTRY_CODE", "Permanent Country Code");
        public static readonly LoanAccountJsonEnum PinCode = new LoanAccountJsonEnum("PinCode", "Pin Code");
        public static readonly LoanAccountJsonEnum NonMailingAddress = new LoanAccountJsonEnum("NONMAILINGADDRESS", "Non-Mailing Address");
        public static readonly LoanAccountJsonEnum AddressType2 = new LoanAccountJsonEnum("ADDRESSTYPE2", "Address Type 2");
        public static readonly LoanAccountJsonEnum EmployerCityCode = new LoanAccountJsonEnum("EMPLOYER_CITY_CODE", "Employer City Code");
        public static readonly LoanAccountJsonEnum EmployerStateCode = new LoanAccountJsonEnum("EMPLOYER_STATE_CODE", "Employer State Code");
        public static readonly LoanAccountJsonEnum Guarantor1County = new LoanAccountJsonEnum("GUARANTOR1_COUNTY", "Guarantor 1 County");
        public static readonly LoanAccountJsonEnum EmployerAddress = new LoanAccountJsonEnum("EMPLOYER_ADDRESS", "Employer Address");
        public static readonly LoanAccountJsonEnum AddOnAddress1 = new LoanAccountJsonEnum("ADD_ON_ADDRESS_1", "Add-On Address 1");
        public static readonly LoanAccountJsonEnum Ref1Address = new LoanAccountJsonEnum("REF1_ADDRS", "Reference 1 Alternate Address");
        public static readonly LoanAccountJsonEnum Ref2Address = new LoanAccountJsonEnum("REF2_ADDRS", "Reference 2 Address");
        public static readonly LoanAccountJsonEnum NonMailingCity = new LoanAccountJsonEnum("NONMAILINGCITY", "Non-Mailing City");
        public static readonly LoanAccountJsonEnum NonMailingLandmark = new LoanAccountJsonEnum("NONMAILINGLANDMARK", "Non-Mailing Landmark");
        public static readonly LoanAccountJsonEnum NonMailingState = new LoanAccountJsonEnum("NONMAILINGSTATE", "Non-Mailing State");
        public static readonly LoanAccountJsonEnum NonMailingZipCode = new LoanAccountJsonEnum("NONMAILINGZIPCODE", "Non-Mailing Zip Code");
        public static readonly LoanAccountJsonEnum CustomerEmployer = new LoanAccountJsonEnum("CUSTOMER_EMPLOYER", "Customer Employer");
        public static readonly LoanAccountJsonEnum CustomerOfficeCountry = new LoanAccountJsonEnum("CUSTOMER_OFFICE_COUNTRY", "Customer Office Country");
        public static readonly LoanAccountJsonEnum CustomerOfficePinCode = new LoanAccountJsonEnum("CUSTOMER_OFFICE_PIN_CODE", "Customer Office Pin Code");
        
        public static readonly LoanAccountJsonEnum UDF1 = new LoanAccountJsonEnum("UDF1", "User Defined Field 1");
        public static readonly LoanAccountJsonEnum UDF2 = new LoanAccountJsonEnum("UDF2", "User Defined Field 2");
        public static readonly LoanAccountJsonEnum UDF3 = new LoanAccountJsonEnum("UDF3", "User Defined Field 3");
        public static readonly LoanAccountJsonEnum UDF4 = new LoanAccountJsonEnum("UDF4", "User Defined Field 4");
        public static readonly LoanAccountJsonEnum UDF5 = new LoanAccountJsonEnum("UDF5", "User Defined Field 5");
        public static readonly LoanAccountJsonEnum UDF6 = new LoanAccountJsonEnum("UDF6", "User Defined Field 6");
        public static readonly LoanAccountJsonEnum UDF7 = new LoanAccountJsonEnum("UDF7", "User Defined Field 7");
        public static readonly LoanAccountJsonEnum UDF8 = new LoanAccountJsonEnum("UDF8", "User Defined Field 8");
        public static readonly LoanAccountJsonEnum UDF9 = new LoanAccountJsonEnum("UDF9", "User Defined Field 9");
        public static readonly LoanAccountJsonEnum UDF10 = new LoanAccountJsonEnum("UDF10", "User Defined Field 10");
        public static readonly LoanAccountJsonEnum UDF11 = new LoanAccountJsonEnum("UDF11", "User Defined Field 11");
        public static readonly LoanAccountJsonEnum UDF12 = new LoanAccountJsonEnum("UDF12", "User Defined Field 12");
        public static readonly LoanAccountJsonEnum UDF13 = new LoanAccountJsonEnum("UDF13", "User Defined Field 13");
        public static readonly LoanAccountJsonEnum UDF14 = new LoanAccountJsonEnum("UDF14", "User Defined Field 14");
        public static readonly LoanAccountJsonEnum UDF15 = new LoanAccountJsonEnum("UDF15", "User Defined Field 15");
        public static readonly LoanAccountJsonEnum UDF16 = new LoanAccountJsonEnum("UDF16", "User Defined Field 16");
        public static readonly LoanAccountJsonEnum UDF17 = new LoanAccountJsonEnum("UDF17", "User Defined Field 17");
        public static readonly LoanAccountJsonEnum UDF18 = new LoanAccountJsonEnum("UDF18", "User Defined Field 18");
        public static readonly LoanAccountJsonEnum UDF19 = new LoanAccountJsonEnum("UDF19", "User Defined Field 19");
        public static readonly LoanAccountJsonEnum UDF20 = new LoanAccountJsonEnum("UDF20", "User Defined Field 20");

        public static readonly LoanAccountJsonEnum LastStatementSentAtAddressOrNotFlag = new LoanAccountJsonEnum("LAST_STATEMENT_SENT_AT_ADDRESS_OR_NOT_FLAG", "Last Statement Sent At Address Or Not Flag");
        public static readonly LoanAccountJsonEnum LastStatementSentAtAddressType = new LoanAccountJsonEnum("LAST_STATEMENT_SENT_AT_ADDRESS_TYPE", "Last Statement Sent At Address Type");
        public static readonly LoanAccountJsonEnum CROName = new LoanAccountJsonEnum("CROName", "CRO Name");

        public static readonly LoanAccountJsonEnum BankAccountNumber = new LoanAccountJsonEnum("BANK_ACC_NUM", "Bank Account Number");
        public static readonly LoanAccountJsonEnum BankCode = new LoanAccountJsonEnum("BANK_CODE", "Bank Code");
        public static readonly LoanAccountJsonEnum BankName = new LoanAccountJsonEnum("BANK_NAME", "Bank Name");
        public static readonly LoanAccountJsonEnum SanctionDate = new LoanAccountJsonEnum("SanctionDate", "Sanction Date");
        public static readonly LoanAccountJsonEnum LoanDate = new LoanAccountJsonEnum("LOAN_DATE", "Loan Date");
        public static readonly LoanAccountJsonEnum DisbursalDate = new LoanAccountJsonEnum("DISBURSALDATE", "Disbursal Date");
        public static readonly LoanAccountJsonEnum TotalPaidToDate = new LoanAccountJsonEnum("TOTAL_PAID_TO_DATE", "Total Paid to Date");
        public static readonly LoanAccountJsonEnum RepaymentStartDate = new LoanAccountJsonEnum("REPAYMENT_STARTDATE", "Repayment Start Date");
        public static readonly LoanAccountJsonEnum ChargeOffDate = new LoanAccountJsonEnum("CHARGEOFF_DATE", "Charge-Off Date");
        public static readonly LoanAccountJsonEnum ModeOfOperation = new LoanAccountJsonEnum("MODE_OF_OPERATION", "Mode of Operation");
        public static readonly LoanAccountJsonEnum LocationCode = new LoanAccountJsonEnum("LOCATION_CODE", "Location Code");
        public static readonly LoanAccountJsonEnum UserClassificationDate = new LoanAccountJsonEnum("USER_CLASSIFICATION_DATE", "User Classification Date");
        public static readonly LoanAccountJsonEnum MainClassificationUser = new LoanAccountJsonEnum("MAIN_CLASSIFICATION_USER", "Main Classification User");

        public static readonly LoanAccountJsonEnum AssetLoanAmount = new LoanAccountJsonEnum("ASSET_LOAN_AMOUNT", "Asset Loan Amount");
        public static readonly LoanAccountJsonEnum PrincipalOutstanding = new LoanAccountJsonEnum("PRINCIPAL_OUTSTANDING", "Principal Outstanding");
        public static readonly LoanAccountJsonEnum TotalInterest = new LoanAccountJsonEnum("TOTAL_INTEREST", "Total Interest");
        public static readonly LoanAccountJsonEnum InterestAmount = new LoanAccountJsonEnum("INTEREST_AMOUNT", "Interest Amount");
        public static readonly LoanAccountJsonEnum PenalStatus = new LoanAccountJsonEnum("PENAL_ST", "Penal Status");
        public static readonly LoanAccountJsonEnum OtherChargesOverdue = new LoanAccountJsonEnum("OtherchargesOverdue", "Other Charges Overdue");
        public static readonly LoanAccountJsonEnum LoanLiability = new LoanAccountJsonEnum("LOAN_LIABILITY", "Loan Liability");
        public static readonly LoanAccountJsonEnum SactAmount = new LoanAccountJsonEnum("SACT_AMOUNT", "Sanctioned Amount");
        public static readonly LoanAccountJsonEnum LoanAmount = new LoanAccountJsonEnum("LOAN_AMOUNT", "Loan Amount");
        public static readonly LoanAccountJsonEnum ForeclosePrepayCharge = new LoanAccountJsonEnum("ForeclosePrepayCharge", "Foreclose Prepay Charge");
        public static readonly LoanAccountJsonEnum OtherReceivables = new LoanAccountJsonEnum("OtherReceivables", "Other Receivables");
        public static readonly LoanAccountJsonEnum TotalPos = new LoanAccountJsonEnum("TOTAL_POS", "Total POS");
        public static readonly LoanAccountJsonEnum TotalInsuranceCharge = new LoanAccountJsonEnum("TOTAL_INSURANCE_CHARGE", "Total Insurance Charge");
        public static readonly LoanAccountJsonEnum TotalProcessingCharge = new LoanAccountJsonEnum("TOTAL_PROCESSING_CHARGE", "Total Processing Charge");
        public static readonly LoanAccountJsonEnum TotalValuationCharge = new LoanAccountJsonEnum("TOTAL_VALUATION_CHARGE", "Total Valuation Charge");
        public static readonly LoanAccountJsonEnum OtherCharge = new LoanAccountJsonEnum("OTHER_CHARGE", "Other Charge");
        public static readonly LoanAccountJsonEnum InterestAmountAlt = new LoanAccountJsonEnum("INT_AMOUNT", "Interest Amount Alternate");
        public static readonly LoanAccountJsonEnum LegalCharge = new LoanAccountJsonEnum("LEGAL_CHARGE", "Legal Charge");
        public static readonly LoanAccountJsonEnum PaymentType = new LoanAccountJsonEnum("PAYMENT_TYPE", "Payment Type");
        
        public static readonly LoanAccountJsonEnum LoanPeriodInDays = new LoanAccountJsonEnum("LoanPeriodInDays", "Loan Period In Days");
        public static readonly LoanAccountJsonEnum TenureMonth = new LoanAccountJsonEnum("TENURE_MONTH", "Tenure Month");
        public static readonly LoanAccountJsonEnum TenureDays = new LoanAccountJsonEnum("Tenure_Days", "Tenure Days");

        public static readonly LoanAccountJsonEnum DELSTRING = new LoanAccountJsonEnum("DELSTRING", "Del String");
        public static readonly LoanAccountJsonEnum CreditBureauScore = new LoanAccountJsonEnum("CreditBureauScore", "Credit Bureau Score");
        public static readonly LoanAccountJsonEnum CustomerBehaviourScore1 = new LoanAccountJsonEnum("CustomerBehaviourScore1", "Customer Behaviour Score 1");
        public static readonly LoanAccountJsonEnum CustomerBehaviourScore2 = new LoanAccountJsonEnum("CustomerBehaviourScore2", "Customer Behaviour Score 2");
        public static readonly LoanAccountJsonEnum EarlyWarningScore = new LoanAccountJsonEnum("EarlyWarningScore", "Early Warning Score");
        public static readonly LoanAccountJsonEnum CustomerBehaviorScoreToKeepHisWord = new LoanAccountJsonEnum("CustomerBehaviorScoreToKeepHisWord", "Customer Behavior Score to Keep His Word");
        public static readonly LoanAccountJsonEnum PreferredModeOfPayment = new LoanAccountJsonEnum("PreferredModeOfPayment", "Preferred Mode of Payment");
        public static readonly LoanAccountJsonEnum PropensityToPayOnline = new LoanAccountJsonEnum("PropensityToPayOnline", "Propensity to Pay Online");
        public static readonly LoanAccountJsonEnum DigitalContactabilityScore = new LoanAccountJsonEnum("DigitalContactabilityScore", "Digital Contactability Score");
        public static readonly LoanAccountJsonEnum CallContactabilityScore = new LoanAccountJsonEnum("CallContactabilityScore", "Call Contactability Score");
        public static readonly LoanAccountJsonEnum FieldContactabilityScore = new LoanAccountJsonEnum("FieldContactabilityScore", "Field Contactability Score");
        public static readonly LoanAccountJsonEnum PreferredLanguageId = new LoanAccountJsonEnum("PreferredLanguageId", "Preferred Language Id");
        public static readonly LoanAccountJsonEnum EWSScore = new LoanAccountJsonEnum("EWS_Score", "EWS Score");

        public static readonly LoanAccountJsonEnum ResidentialCustomerCity = new LoanAccountJsonEnum("RESIDENTIAL_CUSTOMER_CITY", "Residential Customer City");
        public static readonly LoanAccountJsonEnum ResidentialCustomerState = new LoanAccountJsonEnum("RESIDENTIAL_CUSTOMER_STATE", "Residential Customer State");

        
        public static readonly LoanAccountJsonEnum Area = new LoanAccountJsonEnum("Area", "Area");
        public static readonly LoanAccountJsonEnum Zone = new LoanAccountJsonEnum("ZONE", "Zone");
        public static readonly LoanAccountJsonEnum Region = new LoanAccountJsonEnum("Region", "Region");
        public static readonly LoanAccountJsonEnum State = new LoanAccountJsonEnum("STATE", "State");
        public static readonly LoanAccountJsonEnum City = new LoanAccountJsonEnum("CITY", "City");

        public static readonly LoanAccountJsonEnum Make = new LoanAccountJsonEnum("MAKE", "Make");
        public static readonly LoanAccountJsonEnum ChasisNumber = new LoanAccountJsonEnum("CHASISNUM", "Chasis Number");
        public static readonly LoanAccountJsonEnum EngineNumber = new LoanAccountJsonEnum("ENGINENUM", "Engine Number");
        public static readonly LoanAccountJsonEnum Registration = new LoanAccountJsonEnum("REGISTRATION", "Registration");
        public static readonly LoanAccountJsonEnum ModelId = new LoanAccountJsonEnum("MODELID", "Model ID");
        public static readonly LoanAccountJsonEnum ManufacturerDescription = new LoanAccountJsonEnum("MANUFACTURERDESC", "Manufacturer Description");
        public static readonly LoanAccountJsonEnum RegistrationNumber = new LoanAccountJsonEnum("REGDNUM", "Registration Number");
        public static readonly LoanAccountJsonEnum UnitDescription = new LoanAccountJsonEnum("UNIT_DESC", "Unit Description");
        public static readonly LoanAccountJsonEnum MarketValue = new LoanAccountJsonEnum("MARKET_VALUE", "Market Value");
        public static readonly LoanAccountJsonEnum SecurityType = new LoanAccountJsonEnum("SECURITY_TYPE", "Security Type");
        public static readonly LoanAccountJsonEnum LevelDescription = new LoanAccountJsonEnum("Level_Desc", "Level Description");
        public static readonly LoanAccountJsonEnum SalesPointCode = new LoanAccountJsonEnum("SalesPointCode", "Sales Point Code");
        public static readonly LoanAccountJsonEnum SalesPointName = new LoanAccountJsonEnum("SALSEPOINTNAME", "Sales Point Name");
        public static readonly LoanAccountJsonEnum BuildingValuation = new LoanAccountJsonEnum("BUILDING_VALUATION", "Building Valuation");
        public static readonly LoanAccountJsonEnum ValuationDate = new LoanAccountJsonEnum("VALUATION_DATE", "Valuation Date");
        public static readonly LoanAccountJsonEnum PropertyVerificationTag = new LoanAccountJsonEnum("PropertyVerificationTag", "Property Verification Tag");

        
        public static readonly LoanAccountJsonEnum LoanStatus = new LoanAccountJsonEnum("LOAN_STATUS", "Loan Status");
        public static readonly LoanAccountJsonEnum PaymentStatus = new LoanAccountJsonEnum("PAYMENTSTATUS", "Payment Status");

       

    }
}