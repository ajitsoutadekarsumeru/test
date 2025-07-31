using System.ComponentModel.DataAnnotations;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetTeleCallerAccountDetails : FlexiQueryBridgeAsync<LoanAccount, GetTeleCallerAccountDetailsDto>
    {
        protected readonly ILogger<GetTeleCallerAccountDetails> _logger;
        protected GetTeleCallerAccountDetailsParams _params;
        protected readonly IRepoFactory _repoFactory;
        private readonly ICustomUtility _customUtility;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetTeleCallerAccountDetails(ILogger<GetTeleCallerAccountDetails> logger, IRepoFactory repoFactory, ICustomUtility customUtility)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _customUtility = customUtility;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual GetTeleCallerAccountDetails AssignParameters(GetTeleCallerAccountDetailsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<GetTeleCallerAccountDetailsDto> Fetch()
        {
            _repoFactory.Init(_params);
            GetTeleCallerAccountDetailsDto model = new GetTeleCallerAccountDetailsDto();
            Dictionary<dynamic, dynamic> accountData = new Dictionary<dynamic, dynamic>();
            Dictionary<string, string> stringDict = new Dictionary<string, string>();
            LoanAccountJSON json = new LoanAccountJSON();
            string productCode = await FetchProductCodeByAccountId(_params.Id);
            Dictionary<string, string> listacc = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(productCode) && string.Equals(productCode, ProductCodeEnum.CreditCard.Value, StringComparison.OrdinalIgnoreCase))
            {
                var account = await Build<LoanAccount>().ToListAsync();
                if (account != null)
                {
                    json = await _repoFactory.GetRepo().FindAll<LoanAccountJSON>().Where(x => x.AccountId == account.FirstOrDefault().Id).FirstOrDefaultAsync();

                    accountData = JsonConvert.DeserializeObject<Dictionary<dynamic, dynamic>>(json.AccountJSON);
                }
                model = account
                        .Select(a => new GetTeleCallerAccountDetailsDto
                        {
                            Account = new TelecallerAccountOutputApiModel
                            {
                                AccountJSON = json?.AccountJSON ?? null,
                                CaseDetails = new TelecallerAccountDetailsOutputApiModel
                                {
                                    AGREEMENTID = a.AGREEMENTID,
                                    LenderId = a.LenderId,
                                    Partner_Loan_ID = a.Partner_Loan_ID,
                                    Partner_Customer_ID = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PartnerCustomerID.Value),
                                    Partner_Name = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PartnerName.Value),
                                    CUSTOMERID = a.CUSTOMERID,
                                    CUSTOMERNAME = a.CUSTOMERNAME,
                                    ProductGroup = a.ProductGroup,
                                    ProductCode = a.ProductCode,
                                    PRODUCT = a.PRODUCT,
                                    SubProduct = a.SubProduct,
                                    BANK_ACC_NUM = _customUtility.GetValue(accountData, LoanAccountJsonEnum.BankAccountNumber.Value),
                                    BANK_CODE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.BankCode.Value),
                                    BANK_NAME = _customUtility.GetValue(accountData, LoanAccountJsonEnum.BankName.Value),
                                    SanctionDate = _customUtility.GetValue(accountData, LoanAccountJsonEnum.SanctionDate.Value),
                                    LOAN_DATE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.LoanDate.Value),
                                    DISBURSALDATE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.DisbursalDate.Value),
                                    NEXT_DUE_DATE = a.NEXT_DUE_DATE,
                                    TOTAL_PAID_TO_DATE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.TotalPaidToDate.Value),
                                    LAST_PAYMENT_DATE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.LastPaymentDate.Value),
                                    DueDate = a.DueDate,
                                    REPAYMENT_STARTDATE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.RepaymentStartDate.Value),
                                    WRITEOFFDATE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.WriteOffDate.Value),
                                    EMI_START_DATE = a.EMI_START_DATE,
                                    CHARGEOFF_DATE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.ChargeOffDate.Value),
                                    OVERDUE_DATE = a.OVERDUE_DATE,
                                    MODE_OF_OPERATION = _customUtility.GetValue(accountData, LoanAccountJsonEnum.ModeOfOperation.Value),
                                    LOCATION_CODE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.LocationCode.Value),
                                    SCHEME_DESC = a.SCHEME_DESC,
                                    USER_CLASSIFICATION_DATE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.UserClassificationDate.Value),
                                    MAIN_CLASSIFICATION_USER = _customUtility.GetValue(accountData, LoanAccountJsonEnum.MainClassificationUser.Value),
                                    ECS_ENABLED = _customUtility.GetValue(accountData, LoanAccountJsonEnum.ECSEnabled.Value),
                                    WRITE_OFF_AMOUNT = _customUtility.GetValue(accountData, LoanAccountJsonEnum.WriteOffAmount.Value),
                                    IsEligibleForSettlement = a.IsEligibleForSettlement,
                                    IsEligibleForLegal = a.IsEligibleForLegal,
                                    IsEligibleForRepossession = a.IsEligibleForRepossession,
                                    IsEligibleForRestructure = a.IsEligibleForRestructure,
                                    RephasementFlag = _customUtility.GetValue(accountData, LoanAccountJsonEnum.RephasementFlag.Value),
                                },
                                CaseFinancials = new TelecallerAccountFinancialsOutputApiModel
                                {
                                    BCC_PENDING = _customUtility.GetValue(accountData, LoanAccountJsonEnum.BCCPending.Value),
                                    ASSET_LOAN_AMOUNT = _customUtility.GetValue(accountData, LoanAccountJsonEnum.AssetLoanAmount.Value),
                                    EMIAMT = a.EMIAMT,
                                    PRINCIPAL_OUTSTANDING = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PrincipalOutstanding.Value),
                                    TOTAL_INTEREST = _customUtility.GetValue(accountData, LoanAccountJsonEnum.TotalInterest.Value),
                                    INTEREST_OD = a.INTEREST_OD,
                                    INTEREST_AMOUNT = _customUtility.GetValue(accountData, LoanAccountJsonEnum.InterestAmount.Value),
                                    PENAL_ST = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PenalStatus.Value),
                                    OtherchargesOverdue = _customUtility.GetValue(accountData, LoanAccountJsonEnum.OtherChargesOverdue.Value),
                                    PENAL_PENDING = a.PENAL_PENDING,
                                    LOAN_LIABILITY = _customUtility.GetValue(accountData, LoanAccountJsonEnum.LoanLiability.Value),
                                    EMI_OD_AMT = a.EMI_OD_AMT,
                                    CURRENT_POS = a.CURRENT_POS,
                                    NEXT_DUE_AMOUNT = a.NEXT_DUE_AMOUNT,
                                    BOUNCED_CHEQUE_CHARGES = _customUtility.GetValue(accountData, LoanAccountJsonEnum.BouncedChequeCharges.Value),
                                    LAST_PAYMENT_AMOUNT = _customUtility.GetValue(accountData, LoanAccountJsonEnum.LastPaymentAmount.Value),
                                    PRINCIPAL_OD = a.PRINCIPAL_OD,
                                    BOM_POS = a.BOM_POS,
                                    SACT_AMOUNT = _customUtility.GetValue(accountData, LoanAccountJsonEnum.SactAmount.Value),
                                    DISBURSEDAMOUNT = a.DISBURSEDAMOUNT,
                                    LOAN_AMOUNT = _customUtility.GetValue(accountData, LoanAccountJsonEnum.LoanAmount.Value),
                                    OTHER_CHARGES = a.OTHER_CHARGES,
                                    ForeclosePrepayCharge = _customUtility.GetValue(accountData, LoanAccountJsonEnum.ForeclosePrepayCharge.Value),
                                    TOS = a.TOS,
                                    TOTAL_ARREARS = a.TOTAL_ARREARS,
                                    OtherReceivables = _customUtility.GetValue(accountData, LoanAccountJsonEnum.OtherReceivables.Value),
                                    Excess = a.Excess,
                                    TOTAL_POS = _customUtility.GetValue(accountData, LoanAccountJsonEnum.TotalPos.Value),
                                    TOTAL_BOUNCE_CHARGE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.TotalBounceCharge.Value),
                                    TOTAL_LATE_PAYMENT_CHARGE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.TotalLatePaymentCharge.Value),
                                    TOTAL_INSURANCE_CHARGE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.TotalInsuranceCharge.Value),
                                    TOTAL_PROCESSING_CHARGE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.TotalProcessingCharge.Value),
                                    TOTAL_VALUATION_CHARGE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.TotalValuationCharge.Value),
                                    OTHER_CHARGE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.OtherCharge.Value),
                                    INT_AMOUNT = _customUtility.GetValue(accountData, LoanAccountJsonEnum.InterestAmountAlt.Value),
                                    TOTAL_OVERDUE_AMT = _customUtility.GetValue(accountData, LoanAccountJsonEnum.TotalOverdueAmount.Value),
                                    LEGAL_CHARGE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.LegalCharge.Value),
                                    PAYMENT_TYPE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PaymentType.Value),
                                },
                                CaseStatus = new TelecallerAccountStatusOutputApiModel
                                {
                                    OVERDUE_DAYS = a.OVERDUE_DAYS,
                                    CURRENT_DPD = a.CURRENT_DPD,
                                    CURRENT_BUCKET = a.CURRENT_BUCKET,
                                    BUCKET = a.BUCKET,
                                    NPA_STAGEID = a.NPA_STAGEID,
                                    PAYMENTSTATUS = a.PAYMENTSTATUS,
                                    Month_on_Books = _customUtility.GetValue(accountData, LoanAccountJsonEnum.MonthOnBooks.Value),
                                    NO_OF_EMI_OD = a.NO_OF_EMI_OD,
                                    LoanPeriodInDays = _customUtility.GetValue(accountData, LoanAccountJsonEnum.LoanPeriodInDays.Value),
                                    TENURE_MONTH = _customUtility.GetValue(accountData, LoanAccountJsonEnum.TenureMonth.Value),
                                    Tenure_Days = _customUtility.GetValue(accountData, LoanAccountJsonEnum.TenureDays.Value),
                                    LOAN_STATUS = a.LOAN_STATUS
                                },
                                Analytics = new TelecallerAccountAnalyticsOutputAPIModel
                                {
                                    CustomerPersona = a.CustomerPersona,
                                    PTPDate = a.PTPDate,
                                    DELSTRING = _customUtility.GetValue(accountData, LoanAccountJsonEnum.DELSTRING.Value),
                                    PropensityToPay = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PropensityToPay.Value),
                                    PropensityToPayConfidence = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PropensityToPayConfidence.Value),
                                    IsDNDEnabled = a.IsDNDEnabled,
                                    DispCode = a.DispCode,
                                    SegmentationCode = _customUtility.GetValue(accountData, LoanAccountJsonEnum.SegmentationCode.Value),
                                    PAR = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PAR.Value),
                                    CreditBureauScore = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CreditBureauScore.Value),
                                    CustomerBehaviourScore1 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CustomerBehaviourScore1.Value),
                                    CustomerBehaviourScore2 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CustomerBehaviourScore2.Value),
                                    EarlyWarningScore = _customUtility.GetValue(accountData, LoanAccountJsonEnum.EarlyWarningScore.Value),
                                    CustomerBehaviorScoreToKeepHisWord = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CustomerBehaviorScoreToKeepHisWord.Value),
                                    PreferredModeOfPayment = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PreferredModeOfPayment.Value),
                                    PropensityToPayOnline = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PropensityToPayOnline.Value),
                                    DigitalContactabilityScore = _customUtility.GetValue(accountData, LoanAccountJsonEnum.DigitalContactabilityScore.Value),
                                    CallContactabilityScore = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CallContactabilityScore.Value),
                                    FieldContactabilityScore = _customUtility.GetValue(accountData, LoanAccountJsonEnum.FieldContactabilityScore.Value),
                                    PreferredLanguageId = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PreferredLanguageId.Value),
                                    EWS_Score = _customUtility.GetValue(accountData, LoanAccountJsonEnum.EWSScore.Value),
                                },
                                OtherContactDetails = new TelecallerAccountOtherContactDetailsOutputAPIModel
                                {
                                    PinCode = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PinCode.Value),
                                    NONMAILINGADDRESS = _customUtility.GetValue(accountData, LoanAccountJsonEnum.NonMailingAddress.Value),
                                    ADDRESSTYPE2 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.AddressType2.Value),
                                    EMPLOYER_CITY_CODE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.EmployerCityCode.Value),
                                    EMPLOYER_STATE_CODE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.EmployerStateCode.Value),
                                    GUARANTOR1_CITY = _customUtility.GetValue(accountData, LoanAccountJsonEnum.Guarantor1City.Value),
                                    GUARANTOR1_COUNTY = _customUtility.GetValue(accountData, LoanAccountJsonEnum.Guarantor1County.Value),
                                    FATHERS_ADDRS = _customUtility.GetValue(accountData, LoanAccountJsonEnum.FathersAddress.Value),
                                    MOTHERS_ADDRS = _customUtility.GetValue(accountData, LoanAccountJsonEnum.MothersAddress.Value),
                                    EMPLOYER_ADDRESS = _customUtility.GetValue(accountData, LoanAccountJsonEnum.EmployerAddress.Value),
                                    ADD_ON_ADDRESS_1 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.AddOnAddress1.Value),
                                    REFERENCE1_ADDRS = _customUtility.GetValue(accountData, LoanAccountJsonEnum.Reference1Address.Value),
                                    REF1_ADDRS = _customUtility.GetValue(accountData, LoanAccountJsonEnum.Ref1Address.Value),
                                    REF2_ADDRS = _customUtility.GetValue(accountData, LoanAccountJsonEnum.Ref2Address.Value),
                                    NONMAILINGCITY = _customUtility.GetValue(accountData, LoanAccountJsonEnum.NonMailingCity.Value),
                                    NONMAILINGLANDMARK = _customUtility.GetValue(accountData, LoanAccountJsonEnum.NonMailingLandmark.Value),
                                    NONMAILINGSTATE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.NonMailingState.Value),
                                    NONMAILINGZIPCODE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.NonMailingZipCode.Value),
                                    PERMANENT_COUNTRY_CODE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PermanentCountryCode.Value),
                                    CUSTOMER_EMPLOYER = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CustomerEmployer.Value),
                                    CUSTOMER_OFFICE_COUNTRY = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CustomerOfficeCountry.Value),
                                    CUSTOMER_OFFICE_PIN_CODE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CustomerOfficePinCode.Value),
                                    RESIDENTIAL_CUSTOMER_CITY = _customUtility.GetValue(accountData, LoanAccountJsonEnum.ResidentialCustomerCity.Value),
                                    RESIDENTIAL_CUSTOMER_STATE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.ResidentialCustomerState.Value),
                                },
                                DemographicDetails = new TelecallerAccountDemographicOutputApiModel
                                {
                                    DateOfBirth = a.DateOfBirth,
                                    GENDER = _customUtility.GetValue(accountData, LoanAccountJsonEnum.Gender.Value),
                                    EMAIL_ID = a.EMAIL_ID,
                                    PAN_CARD_DETAILS = a.PAN_CARD_DETAILS,
                                    MaritalStatus = _customUtility.GetValue(accountData, LoanAccountJsonEnum.MaritalStatus.Value),
                                    SZ_SPOUSE_NAME = _customUtility.GetValue(accountData, LoanAccountJsonEnum.SZSpouseName.Value),
                                    NameOfBusiness = _customUtility.GetValue(accountData, LoanAccountJsonEnum.NameOfBusiness.Value),
                                    RegistrationNo = _customUtility.GetValue(accountData, LoanAccountJsonEnum.RegistrationNo.Value),
                                    TAXIdentificationNumber = _customUtility.GetValue(accountData, LoanAccountJsonEnum.TAXIdentificationNumber.Value),
                                    Area = _customUtility.GetValue(accountData, LoanAccountJsonEnum.Area.Value),
                                    COMMUNICATION_COUNTRY_CODE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CommunicationCountryCode.Value),
                                    ZONE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.Zone.Value),
                                    Region = _customUtility.GetValue(accountData, LoanAccountJsonEnum.Region.Value),
                                    STATE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.State.Value),
                                    CITY = _customUtility.GetValue(accountData, LoanAccountJsonEnum.City.Value),
                                    CityTier = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CityTier.Value),
                                    BRANCH = a.BRANCH,
                                    BranchCode = a.BranchCode,
                                    //BRANCH_CODE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.bra.Value),
                                    MAILINGADDRESS = _customUtility.GetValue(accountData, LoanAccountJsonEnum.MailingAddress.Value),
                                    MAILINGZIPCODE = a.MAILINGZIPCODE,
                                    MAILINGPHONE1 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.MailingPhone1.Value),
                                    MAILINGPHONE2 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.MailingPhone2.Value),
                                    MAILINGMOBILE = a.MAILINGMOBILE,
                                    ADDRESSTYPE1 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.AddressType1.Value),
                                    FATHERS_CONTACTS = _customUtility.GetValue(accountData, LoanAccountJsonEnum.FathersContacts.Value),
                                    GUARANTOR_1 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.Guarantor1.Value),
                                    GUARANTOR_2 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.Guarantor2.Value),
                                    GUARANTOR_3 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.Guarantor3.Value),
                                    GUARANTOR_4 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.Guarantor4.Value),
                                    CO_APPLICANT1_NAME = a.CO_APPLICANT1_NAME,
                                    CO_APPLICANT1_CONTACT = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CoApplicant1Contact.Value),
                                    CO_APPLICANT_1 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CoApplicant1.Value),
                                    CO_APPLICANT_2 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CoApplicant2.Value),
                                    CO_APPLICANT_3 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CoApplicant3.Value),
                                    CO_APPLICANT_4 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CoApplicant4.Value),
                                    FATHERNAME = _customUtility.GetValue(accountData, LoanAccountJsonEnum.FatherName.Value),
                                    MOTHERS_CONTACTS = _customUtility.GetValue(accountData, LoanAccountJsonEnum.MothersContacts.Value),
                                    MOTHERS_NAME = _customUtility.GetValue(accountData, LoanAccountJsonEnum.MothersName.Value),
                                    REF1_CONTACT = _customUtility.GetValue(accountData, LoanAccountJsonEnum.Ref1Contact.Value),
                                    REFERENCE1_NAME = _customUtility.GetValue(accountData, LoanAccountJsonEnum.Reference1Name.Value),
                                    REFERENCE2_NAME = _customUtility.GetValue(accountData, LoanAccountJsonEnum.Reference2Name.Value),
                                    NONMAILINGPHONE1 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.NonMailingPhone1.Value),
                                    NONMAILINGPHONE2 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.NonMailingPhone2.Value),
                                    REF2_CONTACT = _customUtility.GetValue(accountData, LoanAccountJsonEnum.Ref2Contact.Value),
                                    NONMAILINGMOBILE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.NonMailingMobile.Value),
                                    NewAddress = _customUtility.GetValue(accountData, LoanAccountJsonEnum.NewAddress.Value),
                                    LatestMobileNo = a.LatestMobileNo,
                                },
                                OtherDetails = new TelecallerAccountOtherDetailsOutputAPIModel
                                {
                                    UDF1 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.UDF1.Value),
                                    UDF2 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.UDF2.Value),
                                    UDF3 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.UDF3.Value),
                                    UDF4 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.UDF4.Value),
                                    UDF5 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.UDF5.Value),
                                    UDF6 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.UDF6.Value),
                                    UDF7 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.UDF7.Value),
                                    UDF8 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.UDF8.Value),
                                    UDF9 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.UDF9.Value),
                                    UDF10 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.UDF10.Value),
                                    UDF11 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.UDF11.Value),
                                    UDF12 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.UDF12.Value),
                                    UDF13 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.UDF13.Value),
                                    UDF14 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.UDF14.Value),
                                    UDF15 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.UDF15.Value),
                                    UDF16 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.UDF16.Value),
                                    UDF17 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.UDF17.Value),
                                    UDF18 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.UDF18.Value),
                                    UDF19 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.UDF19.Value),
                                    UDF20 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.UDF20.Value),
                                },
                                AssetDetails = new TelecallerAccountAssetDetailsOutputAPIModel
                                {
                                    PHYSICAL_ADDRESS = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PhysicalAddress.Value),
                                    MONTH = a.MONTH,
                                    YEAR = a.YEAR,
                                    MAKE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.Make.Value),
                                    CHASISNUM = _customUtility.GetValue(accountData, LoanAccountJsonEnum.ChasisNumber.Value),
                                    ENGINENUM = _customUtility.GetValue(accountData, LoanAccountJsonEnum.EngineNumber.Value),
                                    REGISTRATION = _customUtility.GetValue(accountData, LoanAccountJsonEnum.Registration.Value),
                                    MODELID = _customUtility.GetValue(accountData, LoanAccountJsonEnum.ModelId.Value),
                                    MANUFACTURERDESC = _customUtility.GetValue(accountData, LoanAccountJsonEnum.ManufacturerDescription.Value),
                                    REGDNUM = _customUtility.GetValue(accountData, LoanAccountJsonEnum.RegistrationNumber.Value),
                                    UNIT_DESC = _customUtility.GetValue(accountData, LoanAccountJsonEnum.UnitDescription.Value),
                                    MARKET_VALUE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.MarketValue.Value),
                                    SECURITY_TYPE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.SecurityType.Value),
                                    Level_Desc = _customUtility.GetValue(accountData, LoanAccountJsonEnum.LevelDescription.Value),
                                    AssetType = _customUtility.GetValue(accountData, LoanAccountJsonEnum.AssetType.Value),
                                    AssetDetails = _customUtility.GetValue(accountData, LoanAccountJsonEnum.AssetDetails.Value),
                                    AssetName = _customUtility.GetValue(accountData, LoanAccountJsonEnum.AssetName.Value),
                                    GroupId = a.GroupId,
                                    GroupName = a.GroupName,
                                    CentreID = a.CentreID,
                                    CentreName = a.CentreName,
                                    CROName = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CROName.Value),
                                    SalesPointCode = _customUtility.GetValue(accountData, LoanAccountJsonEnum.SalesPointCode.Value),
                                    SALSEPOINTNAME = _customUtility.GetValue(accountData, LoanAccountJsonEnum.SalesPointName.Value),
                                    BUILDING_VALUATION = _customUtility.GetValue(accountData, LoanAccountJsonEnum.BuildingValuation.Value),
                                    VALUATION_DATE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.ValuationDate.Value),
                                    PropertyVerificationTag = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PropertyVerificationTag.Value),
                                },
                                ContactDetails = new AccountLatestContactDetailsDto
                                {
                                    Latest_Address_From_Trail = a.Latest_Address_From_Trail,
                                    Latest_Email_From_Trail = a.Latest_Email_From_Trail,
                                    Latest_Number_From_Trail = a.Latest_Number_From_Trail,
                                    Latest_Number_From_Receipt = a.Latest_Number_From_Receipt,
                                    Latest_Number_From_Send_Payment = a.Latest_Number_From_Send_Payment
                                }
                            },
                            Allocations = new AllocationHistoryOutputApiModel
                            {
                                SourcingAgentName = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CROName.Value),
                                AllocationOwnerCode = a.AllocationOwner?.CustomId ?? string.Empty,
                                AllocationOwnerName = $"{a.AllocationOwner?.FirstName ?? string.Empty} {a.AllocationOwner?.LastName ?? string.Empty}".Trim(),
                                TelecallingAgencyName = a.TeleCallingAgency?.FirstName ?? string.Empty, // + " " + a.TeleCallingAgency?.LastName ?? string.Empty
                                TelecallingAgencyCode = a.TeleCallingAgency?.CustomId ?? string.Empty,
                                TelecallerName = $"{a.TeleCaller?.FirstName ?? string.Empty} {a.TeleCaller?.LastName ?? string.Empty}".Trim(),
                                TelecallerCode = a.TeleCaller?.CustomId ?? string.Empty,
                                AgencyName = a.Agency?.FirstName ?? string.Empty, // + " " + a.Agency?.LastName ?? string.Empty
                                AgencyCode = a.Agency?.CustomId ?? string.Empty,
                                AgentName = $"{a.Collector?.FirstName ?? string.Empty} {a.Collector?.LastName ?? string.Empty}".Trim(),
                                AgentCode = a.Collector?.CustomId ?? string.Empty
                            },
                            //CreditScore = a.ENCollectCreditBureauDetails
                        }).FirstOrDefault();

                var collections1 = await _repoFactory.GetRepo().FindAll<Collection>().Where(i => i.AccountId == _params.Id)
                       .Select(c => new TelecallerDashboardCollectionsOutputApiModel
                       {
                           Id = c.Id,
                           ReceiptNo = c.CustomId,
                           PaymentDate = c.CollectionDate,
                           Amount = c.Amount,
                           CollectionMode = c.CollectionMode,
                           InstrumentNumber = c.Cheque.InstrumentNo,
                           BankName = c.Cheque.BankName,
                           BranchName = c.Cheque.BranchName,
                           CollectorFirstName = c.Collector.FirstName,
                           CollectorLastName = c.Collector.LastName,
                           PayerImageName = c.PayerImageName,
                           ChangeRequestImageName = c.ChangeRequestImageName,
                           PaymentStatus = (c.CollectionBatch != null) ? (c.CollectionBatch.PayInSlip != null ? c.CollectionBatch.PayInSlip.PayInSlipWorkflowState.Name : c.CollectionWorkflowState.Name) : c.CollectionWorkflowState.Name
                       }).OrderByDescending(z => z.PaymentDate).ToListAsync();

                model.CollectionHistory = new List<TelecallerDashboardCollectionsOutputApiModel>();
                if (collections1 != null)
                {
                    model.CollectionHistory = collections1;
                }

                var feedbacks1 = await _repoFactory.GetRepo().FindAll<Feedback>().Where(i => i.AccountId == _params.Id)

                                           .Select(f => new TelecallerDashboardFeedBacksHistoryOutputApiModel
                                           {
                                               Id = f.Id,
                                               CustomerMet = f.CustomerMet,
                                               DispositionCode = f.DispositionCode,
                                               PTPDate = f.PTPDate,
                                               PTPAmount = f.PTPAmount,
                                               EscalateTo = f.EscalateTo,
                                               IsReallocationRequest = f.IsReallocationRequest,
                                               ReallocationRequestReason = f.ReallocationRequestReason,
                                               NewArea = f.NewArea,
                                               NewAddress = f.NewAddress,
                                               City = f.City,
                                               NewContact = f.NewContactNo,
                                               Remarks = f.Remarks,
                                               CollectorFirstName = f.Collector.FirstName,
                                               CollectorLastName = f.Collector.LastName,
                                               DispositionDate = f.DispositionDate,
                                               UploadedFileName = f.UploadedFileName,
                                               DispositionGroup = f.DispositionGroup,
                                               PickAddress = f.PickAddress,
                                               OtherPickAddress = f.OtherPickAddress,
                                               AgentContactNo = f.CreatedBy,
                                               Place_of_visit = f.Place_of_visit,
                                               ThirdPartyContactPerson = f.ThirdPartyContactPerson,
                                               NonPaymentReason = f.NonPaymentReason,
                                               AssignReason = f.AssignReason
                                           }).OrderByDescending(p => p.DispositionDate).ToListAsync();

                model.FeedBackHistory = new List<TelecallerDashboardFeedBacksHistoryOutputApiModel>();
                if (feedbacks1 != null)
                {
                    List<string> ids = feedbacks1.Select(c => c.AgentContactNo).ToList();
                    var result = await _repoFactory.GetRepo().FindAll<ApplicationUser>().Where(x => ids.Contains(x.Id)).Select(u => new { u.Id, u.PrimaryMobileNumber }).ToListAsync();
                    // Create a dictionary to map IDs to user names
                    var userMap = result.ToDictionary(u => u.Id, u => $"{u.PrimaryMobileNumber}");

                    foreach (var feedback in feedbacks1)
                    {
                        if (userMap.TryGetValue(feedback.AgentContactNo, out string mobileNo))
                        {
                            feedback.AgentContactNo = mobileNo;
                        }
                    }

                    model.FeedBackHistory = feedbacks1;
                }

                if (model != null && model.Allocations != null && model.Allocations.AllocationOwnerCode != null)
                {
                    var user = await _repoFactory.GetRepo().FindAll<CompanyUser>().Where(x => x.CustomId == model.Allocations.AllocationOwnerCode).FirstOrDefaultAsync();
                    if (user != null && user.Designation != null)
                    {
                        model.Allocations.AllocationOwnerDesignation = user.Designation.FirstOrDefault().Designation.Name;
                    }
                }
            }
            else
            {
                var account = await Build<LoanAccount>().ToListAsync();
                if (account != null)
                {
                    json = await _repoFactory.GetRepo().FindAll<LoanAccountJSON>().Where(x => x.AccountId == account.FirstOrDefault().Id).FirstOrDefaultAsync();

                    accountData = JsonConvert.DeserializeObject<Dictionary<dynamic, dynamic>>(json.AccountJSON);
                }
                model = account
                        .Select(a => new GetTeleCallerAccountDetailsDto
                        {
                            Account = new TelecallerAccountOutputApiModel
                            {
                                AccountJSON = json?.AccountJSON ?? null,
                                CaseDetails = new TelecallerAccountDetailsOutputApiModel
                                {
                                    AGREEMENTID = a.AGREEMENTID,
                                    LenderId = a.LenderId,
                                    Partner_Loan_ID = a.Partner_Loan_ID,
                                    Partner_Customer_ID = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PartnerCustomerID.Value),
                                    Partner_Name = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PartnerName.Value),
                                    CUSTOMERID = a.CUSTOMERID,
                                    CUSTOMERNAME = a.CUSTOMERNAME,
                                    ProductGroup = a.ProductGroup,
                                    ProductCode = a.ProductCode,
                                    PRODUCT = a.PRODUCT,
                                    SubProduct = a.SubProduct,
                                    BANK_ACC_NUM = _customUtility.GetValue(accountData, LoanAccountJsonEnum.BankAccountNumber.Value),
                                    BANK_CODE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.BankCode.Value),
                                    BANK_NAME = _customUtility.GetValue(accountData, LoanAccountJsonEnum.BankName.Value),
                                    SanctionDate = _customUtility.GetValue(accountData, LoanAccountJsonEnum.SanctionDate.Value),
                                    LOAN_DATE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.LoanDate.Value),
                                    DISBURSALDATE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.DisbursalDate.Value),
                                    NEXT_DUE_DATE = a.NEXT_DUE_DATE,
                                    TOTAL_PAID_TO_DATE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.TotalPaidToDate.Value),
                                    LAST_PAYMENT_DATE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.LastPaymentDate.Value),
                                    DueDate = a.DueDate,
                                    REPAYMENT_STARTDATE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.RepaymentStartDate.Value),
                                    WRITEOFFDATE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.WriteOffDate.Value),
                                    EMI_START_DATE = a.EMI_START_DATE,
                                    CHARGEOFF_DATE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.ChargeOffDate.Value),
                                    OVERDUE_DATE = a.OVERDUE_DATE,
                                    MODE_OF_OPERATION = _customUtility.GetValue(accountData, LoanAccountJsonEnum.ModeOfOperation.Value),
                                    LOCATION_CODE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.LocationCode.Value),
                                    SCHEME_DESC = a.SCHEME_DESC,
                                    USER_CLASSIFICATION_DATE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.UserClassificationDate.Value),
                                    MAIN_CLASSIFICATION_USER = _customUtility.GetValue(accountData, LoanAccountJsonEnum.MainClassificationUser.Value),
                                    ECS_ENABLED = _customUtility.GetValue(accountData, LoanAccountJsonEnum.ECSEnabled.Value),
                                    WRITE_OFF_AMOUNT = _customUtility.GetValue(accountData, LoanAccountJsonEnum.WriteOffAmount.Value),
                                    IsEligibleForSettlement = a.IsEligibleForSettlement,
                                    IsEligibleForLegal = a.IsEligibleForLegal,
                                    IsEligibleForRepossession = a.IsEligibleForRepossession,
                                    IsEligibleForRestructure = a.IsEligibleForRestructure,
                                    RephasementFlag = _customUtility.GetValue(accountData, LoanAccountJsonEnum.RephasementFlag.Value),
                                },
                                CaseFinancials = new TelecallerAccountFinancialsOutputApiModel
                                {
                                    BCC_PENDING = _customUtility.GetValue(accountData, LoanAccountJsonEnum.BCCPending.Value),
                                    ASSET_LOAN_AMOUNT = _customUtility.GetValue(accountData, LoanAccountJsonEnum.AssetLoanAmount.Value),
                                    EMIAMT = a.EMIAMT,
                                    PRINCIPAL_OUTSTANDING = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PrincipalOutstanding.Value),
                                    TOTAL_INTEREST = _customUtility.GetValue(accountData, LoanAccountJsonEnum.TotalInterest.Value),
                                    INTEREST_OD = a.INTEREST_OD,
                                    INTEREST_AMOUNT = _customUtility.GetValue(accountData, LoanAccountJsonEnum.InterestAmount.Value),
                                    PENAL_ST = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PenalStatus.Value),
                                    OtherchargesOverdue = _customUtility.GetValue(accountData, LoanAccountJsonEnum.OtherChargesOverdue.Value),
                                    PENAL_PENDING = a.PENAL_PENDING,
                                    LOAN_LIABILITY = _customUtility.GetValue(accountData, LoanAccountJsonEnum.LoanLiability.Value),
                                    EMI_OD_AMT = a.EMI_OD_AMT,
                                    CURRENT_POS = a.CURRENT_POS,
                                    NEXT_DUE_AMOUNT = a.NEXT_DUE_AMOUNT,
                                    BOUNCED_CHEQUE_CHARGES = _customUtility.GetValue(accountData, LoanAccountJsonEnum.BouncedChequeCharges.Value),
                                    LAST_PAYMENT_AMOUNT = _customUtility.GetValue(accountData, LoanAccountJsonEnum.LastPaymentAmount.Value),
                                    PRINCIPAL_OD = a.PRINCIPAL_OD,
                                    BOM_POS = a.BOM_POS,
                                    SACT_AMOUNT = _customUtility.GetValue(accountData, LoanAccountJsonEnum.SactAmount.Value),
                                    DISBURSEDAMOUNT = a.DISBURSEDAMOUNT,
                                    LOAN_AMOUNT = _customUtility.GetValue(accountData, LoanAccountJsonEnum.LoanAmount.Value),
                                    OTHER_CHARGES = a.OTHER_CHARGES,
                                    ForeclosePrepayCharge = _customUtility.GetValue(accountData, LoanAccountJsonEnum.ForeclosePrepayCharge.Value),
                                    TOS = a.TOS,
                                    TOTAL_ARREARS = a.TOTAL_ARREARS,
                                    OtherReceivables = _customUtility.GetValue(accountData, LoanAccountJsonEnum.OtherReceivables.Value),
                                    Excess = a.Excess,
                                    TOTAL_POS = _customUtility.GetValue(accountData, LoanAccountJsonEnum.TotalPos.Value),
                                    TOTAL_BOUNCE_CHARGE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.TotalBounceCharge.Value),
                                    TOTAL_LATE_PAYMENT_CHARGE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.TotalLatePaymentCharge.Value),
                                    TOTAL_INSURANCE_CHARGE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.TotalInsuranceCharge.Value),
                                    TOTAL_PROCESSING_CHARGE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.TotalProcessingCharge.Value),
                                    TOTAL_VALUATION_CHARGE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.TotalValuationCharge.Value),
                                    OTHER_CHARGE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.OtherCharge.Value),
                                    INT_AMOUNT = _customUtility.GetValue(accountData, LoanAccountJsonEnum.InterestAmountAlt.Value),
                                    TOTAL_OVERDUE_AMT = a.CURRENT_TOTAL_AMOUNT_DUE != null ? a.CURRENT_TOTAL_AMOUNT_DUE.ToString() : string.Empty,
                                    LEGAL_CHARGE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.LegalCharge.Value),
                                    PAYMENT_TYPE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PaymentType.Value),
                                },
                                CaseStatus = new TelecallerAccountStatusOutputApiModel
                                {
                                    OVERDUE_DAYS = a.OVERDUE_DAYS,
                                    CURRENT_DPD = a.CURRENT_DPD,
                                    CURRENT_BUCKET = a.CURRENT_BUCKET,
                                    BUCKET = a.BUCKET,
                                    NPA_STAGEID = a.NPA_STAGEID,
                                    PAYMENTSTATUS = a.PAYMENTSTATUS,
                                    Month_on_Books = _customUtility.GetValue(accountData, LoanAccountJsonEnum.MonthOnBooks.Value),
                                    NO_OF_EMI_OD = a.NO_OF_EMI_OD,
                                    LoanPeriodInDays = _customUtility.GetValue(accountData, LoanAccountJsonEnum.LoanPeriodInDays.Value),
                                    TENURE_MONTH = _customUtility.GetValue(accountData, LoanAccountJsonEnum.TenureMonth.Value),
                                    Tenure_Days = _customUtility.GetValue(accountData, LoanAccountJsonEnum.TenureDays.Value),
                                    LOAN_STATUS = _customUtility.GetValue(accountData, LoanAccountJsonEnum.LoanStatus.Value),
                                },
                                Analytics = new TelecallerAccountAnalyticsOutputAPIModel
                                {
                                    CustomerPersona = a.CustomerPersona,
                                    PTPDate = a.PTPDate,
                                    DELSTRING = _customUtility.GetValue(accountData, LoanAccountJsonEnum.DelString.Value),
                                    PropensityToPay = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PropensityToPay.Value),
                                    PropensityToPayConfidence = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PropensityToPayConfidence.Value),
                                    IsDNDEnabled = a.IsDNDEnabled,
                                    DispCode = a.DispCode,
                                    SegmentationCode = _customUtility.GetValue(accountData, LoanAccountJsonEnum.SegmentationCode.Value),
                                    PAR = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PAR.Value),
                                    CreditBureauScore = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CreditBureauScore.Value),
                                    CustomerBehaviourScore1 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CustomerBehaviourScore1.Value),
                                    CustomerBehaviourScore2 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CustomerBehaviourScore2.Value),
                                    EarlyWarningScore = _customUtility.GetValue(accountData, LoanAccountJsonEnum.EarlyWarningScore.Value),
                                    CustomerBehaviorScoreToKeepHisWord = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CustomerBehaviorScoreToKeepHisWord.Value),
                                    PreferredModeOfPayment = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PreferredModeOfPayment.Value),
                                    PropensityToPayOnline = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PropensityToPayOnline.Value),
                                    DigitalContactabilityScore = _customUtility.GetValue(accountData, LoanAccountJsonEnum.DigitalContactabilityScore.Value),
                                    CallContactabilityScore = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CallContactabilityScore.Value),
                                    FieldContactabilityScore = _customUtility.GetValue(accountData, LoanAccountJsonEnum.FieldContactabilityScore.Value),
                                    PreferredLanguageId = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PreferredLanguageId.Value),
                                    EWS_Score = _customUtility.GetValue(accountData, LoanAccountJsonEnum.EWSScore.Value),
                                },
                                OtherContactDetails = new TelecallerAccountOtherContactDetailsOutputAPIModel
                                {
                                    PinCode = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PinCode.Value),
                                    NONMAILINGADDRESS = _customUtility.GetValue(accountData, LoanAccountJsonEnum.NonMailingAddress.Value),
                                    ADDRESSTYPE2 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.AddressType2.Value),
                                    EMPLOYER_CITY_CODE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.EmployerCityCode.Value),
                                    EMPLOYER_STATE_CODE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.EmployerStateCode.Value),
                                    GUARANTOR1_CITY = _customUtility.GetValue(accountData, LoanAccountJsonEnum.Guarantor1City.Value),
                                    GUARANTOR1_COUNTY = _customUtility.GetValue(accountData, LoanAccountJsonEnum.Guarantor1County.Value),
                                    FATHERS_ADDRS = _customUtility.GetValue(accountData, LoanAccountJsonEnum.FathersAddress.Value),
                                    MOTHERS_ADDRS = _customUtility.GetValue(accountData, LoanAccountJsonEnum.MothersAddress.Value),
                                    EMPLOYER_ADDRESS = _customUtility.GetValue(accountData, LoanAccountJsonEnum.EmployerAddress.Value),
                                    ADD_ON_ADDRESS_1 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.AddOnAddress1.Value),
                                    REFERENCE1_ADDRS = _customUtility.GetValue(accountData, LoanAccountJsonEnum.Reference1Address.Value),
                                    REF1_ADDRS = _customUtility.GetValue(accountData, LoanAccountJsonEnum.Ref1Address.Value),
                                    REF2_ADDRS = _customUtility.GetValue(accountData, LoanAccountJsonEnum.Ref2Address.Value),
                                    NONMAILINGCITY = _customUtility.GetValue(accountData, LoanAccountJsonEnum.NonMailingCity.Value),
                                    NONMAILINGLANDMARK = _customUtility.GetValue(accountData, LoanAccountJsonEnum.NonMailingLandmark.Value),
                                    NONMAILINGSTATE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.NonMailingState.Value),
                                    NONMAILINGZIPCODE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.NonMailingZipCode.Value),
                                    PERMANENT_COUNTRY_CODE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PermanentCountryCode.Value),
                                    CUSTOMER_EMPLOYER = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CustomerEmployer.Value),
                                    CUSTOMER_OFFICE_COUNTRY = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CustomerOfficeCountry.Value),
                                    CUSTOMER_OFFICE_PIN_CODE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CustomerOfficePinCode.Value),
                                    RESIDENTIAL_CUSTOMER_CITY = _customUtility.GetValue(accountData, LoanAccountJsonEnum.ResidentialCustomerCity.Value),
                                    RESIDENTIAL_CUSTOMER_STATE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.ResidentialCustomerState.Value),
                                },
                                DemographicDetails = new TelecallerAccountDemographicOutputApiModel
                                {
                                    DateOfBirth = a.DateOfBirth,
                                    GENDER = _customUtility.GetValue(accountData, LoanAccountJsonEnum.Gender.Value),
                                    EMAIL_ID = a.EMAIL_ID,
                                    PAN_CARD_DETAILS = a.PAN_CARD_DETAILS,
                                    MaritalStatus = _customUtility.GetValue(accountData, LoanAccountJsonEnum.MaritalStatus.Value),
                                    SZ_SPOUSE_NAME = _customUtility.GetValue(accountData, LoanAccountJsonEnum.SZSpouseName.Value),
                                    NameOfBusiness = _customUtility.GetValue(accountData, LoanAccountJsonEnum.NameOfBusiness.Value),
                                    RegistrationNo = _customUtility.GetValue(accountData, LoanAccountJsonEnum.RegistrationNo.Value),
                                    TAXIdentificationNumber = _customUtility.GetValue(accountData, LoanAccountJsonEnum.TAXIdentificationNumber.Value),
                                    Area = a.Area,
                                    COMMUNICATION_COUNTRY_CODE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CommunicationCountryCode.Value),
                                    ZONE = a.ZONE,
                                    Region = a.Region,
                                    STATE = a.STATE,
                                    CITY = a.CITY,
                                    CityTier = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CityTier.Value),
                                    BRANCH = a.BRANCH,
                                    BranchCode = a.BranchCode,
                                    BRANCH_CODE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.BranchCodeKey.Value),
                                    MAILINGADDRESS = _customUtility.GetValue(accountData, LoanAccountJsonEnum.MailingAddress.Value),
                                    MAILINGZIPCODE = a.MAILINGZIPCODE,
                                    MAILINGPHONE1 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.MailingPhone1.Value),
                                    MAILINGPHONE2 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.MailingPhone2.Value),
                                    MAILINGMOBILE = a.MAILINGMOBILE,
                                    ADDRESSTYPE1 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.AddressType1.Value),
                                    FATHERS_CONTACTS = _customUtility.GetValue(accountData, LoanAccountJsonEnum.FathersContacts.Value),
                                    GUARANTOR_1 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.Guarantor1.Value),
                                    GUARANTOR_2 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.Guarantor2.Value),
                                    GUARANTOR_3 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.Guarantor3.Value),
                                    GUARANTOR_4 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.Guarantor4.Value),
                                    CO_APPLICANT1_NAME = a.CO_APPLICANT1_NAME,
                                    CO_APPLICANT1_CONTACT = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CoApplicant1Contact.Value),
                                    CO_APPLICANT_1 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CoApplicant1.Value),
                                    CO_APPLICANT_2 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CoApplicant2.Value),
                                    CO_APPLICANT_3 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CoApplicant3.Value),
                                    CO_APPLICANT_4 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CoApplicant4.Value),
                                    FATHERNAME = _customUtility.GetValue(accountData, LoanAccountJsonEnum.FatherName.Value),
                                    MOTHERS_CONTACTS = _customUtility.GetValue(accountData, LoanAccountJsonEnum.MothersContacts.Value),
                                    MOTHERS_NAME = _customUtility.GetValue(accountData, LoanAccountJsonEnum.MothersName.Value),
                                    REF1_CONTACT = _customUtility.GetValue(accountData, LoanAccountJsonEnum.Ref1Contact.Value),
                                    REFERENCE1_NAME = _customUtility.GetValue(accountData, LoanAccountJsonEnum.Reference1Name.Value),
                                    REFERENCE2_NAME = _customUtility.GetValue(accountData, LoanAccountJsonEnum.Reference2Name.Value),
                                    NONMAILINGPHONE1 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.NonMailingPhone1.Value),
                                    NONMAILINGPHONE2 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.NonMailingPhone2.Value),
                                    REF2_CONTACT = _customUtility.GetValue(accountData, LoanAccountJsonEnum.Ref2Contact.Value),
                                    NONMAILINGMOBILE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.NonMailingMobile.Value),
                                    NewAddress = _customUtility.GetValue(accountData, LoanAccountJsonEnum.NewAddress.Value),
                                    LatestMobileNo = a.LatestMobileNo,
                                },
                                OtherDetails = new TelecallerAccountOtherDetailsOutputAPIModel
                                {
                                    UDF1 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.UDF1.Value),
                                    UDF2 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.UDF2.Value),
                                    UDF3 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.UDF3.Value),
                                    UDF4 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.UDF4.Value),
                                    UDF5 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.UDF5.Value),
                                    UDF6 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.UDF6.Value),
                                    UDF7 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.UDF7.Value),
                                    UDF8 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.UDF8.Value),
                                    UDF9 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.UDF9.Value),
                                    UDF10 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.UDF10.Value),
                                    UDF11 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.UDF11.Value),
                                    UDF12 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.UDF12.Value),
                                    UDF13 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.UDF13.Value),
                                    UDF14 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.UDF14.Value),
                                    UDF15 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.UDF15.Value),
                                    UDF16 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.UDF16.Value),
                                    UDF17 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.UDF17.Value),
                                    UDF18 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.UDF18.Value),
                                    UDF19 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.UDF19.Value),
                                    UDF20 = _customUtility.GetValue(accountData, LoanAccountJsonEnum.UDF20.Value),
                                },
                                AssetDetails = new TelecallerAccountAssetDetailsOutputAPIModel
                                {
                                    PHYSICAL_ADDRESS = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PhysicalAddress.Value),
                                    MONTH = a.MONTH,
                                    YEAR = a.YEAR,
                                    MAKE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.Make.Value),
                                    CHASISNUM = _customUtility.GetValue(accountData, LoanAccountJsonEnum.ChasisNumber.Value),
                                    ENGINENUM = _customUtility.GetValue(accountData, LoanAccountJsonEnum.EngineNumber.Value),
                                    REGISTRATION = _customUtility.GetValue(accountData, LoanAccountJsonEnum.Registration.Value),
                                    MODELID = _customUtility.GetValue(accountData, LoanAccountJsonEnum.ModelId.Value),
                                    MANUFACTURERDESC = _customUtility.GetValue(accountData, LoanAccountJsonEnum.ManufacturerDescription.Value),
                                    REGDNUM = _customUtility.GetValue(accountData, LoanAccountJsonEnum.RegistrationNumber.Value),
                                    UNIT_DESC = _customUtility.GetValue(accountData, LoanAccountJsonEnum.UnitDescription.Value),
                                    MARKET_VALUE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.MarketValue.Value),
                                    SECURITY_TYPE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.SecurityType.Value),
                                    Level_Desc = _customUtility.GetValue(accountData, LoanAccountJsonEnum.LevelDescription.Value),
                                    AssetType = _customUtility.GetValue(accountData, LoanAccountJsonEnum.AssetType.Value),
                                    AssetDetails = _customUtility.GetValue(accountData, LoanAccountJsonEnum.AssetDetails.Value),
                                    AssetName = _customUtility.GetValue(accountData, LoanAccountJsonEnum.AssetName.Value),
                                    GroupId = a.GroupId,
                                    GroupName = a.GroupName,
                                    CentreID = a.CentreID,
                                    CentreName = a.CentreName,
                                    CROName = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CROName.Value),
                                    SalesPointCode = _customUtility.GetValue(accountData, LoanAccountJsonEnum.SalesPointCode.Value),
                                    SALSEPOINTNAME = _customUtility.GetValue(accountData, LoanAccountJsonEnum.SalesPointName.Value),
                                    BUILDING_VALUATION = _customUtility.GetValue(accountData, LoanAccountJsonEnum.BuildingValuation.Value),
                                    VALUATION_DATE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.ValuationDate.Value),
                                    PropertyVerificationTag = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PropertyVerificationTag.Value),
                                },
                                ContactDetails = new AccountLatestContactDetailsDto
                                {
                                    Latest_Address_From_Trail = a.Latest_Address_From_Trail,
                                    Latest_Email_From_Trail = a.Latest_Email_From_Trail,
                                    Latest_Number_From_Trail = a.Latest_Number_From_Trail,
                                    Latest_Number_From_Receipt = a.Latest_Number_From_Receipt,
                                    Latest_Number_From_Send_Payment = a.Latest_Number_From_Send_Payment
                                }
                            },
                            Allocations = new AllocationHistoryOutputApiModel
                            {
                                SourcingAgentName = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CROName.Value),
                                AllocationOwnerCode = a.AllocationOwner?.CustomId ?? string.Empty,
                                AllocationOwnerName = $"{a.AllocationOwner?.FirstName ?? string.Empty} {a.AllocationOwner?.LastName ?? string.Empty}".Trim(),
                                TelecallingAgencyName = a.TeleCallingAgency?.FirstName ?? string.Empty, // + " " + a.TeleCallingAgency?.LastName ?? string.Empty
                                TelecallingAgencyCode = a.TeleCallingAgency?.CustomId ?? string.Empty,
                                TelecallerName = $"{a.TeleCaller?.FirstName ?? string.Empty} {a.TeleCaller?.LastName ?? string.Empty}".Trim(),
                                TelecallerCode = a.TeleCaller?.CustomId ?? string.Empty,
                                AgencyName = a.Agency?.FirstName ?? string.Empty, // + " " + a.Agency?.LastName ?? string.Empty
                                AgencyCode = a.Agency?.CustomId ?? string.Empty,
                                AgentName = $"{a.Collector?.FirstName ?? string.Empty} {a.Collector?.LastName ?? string.Empty}".Trim(),
                                AgentCode = a.Collector?.CustomId ?? string.Empty
                            },
                            //CreditScore = a.ENCollectCreditBureauDetails
                        }).FirstOrDefault();

                _logger.LogDebug("TeleCallerDashBoard : Account details fetched");
                _logger.LogDebug("TeleCallerDashBoard : Other details fetched");
                if (model != null && model.Allocations != null && model.Allocations.AllocationOwnerCode != null)
                {
                    var user = await _repoFactory.GetRepo().FindAll<CompanyUser>().Where(x => x.CustomId == model.Allocations.AllocationOwnerCode).FirstOrDefaultAsync();
                    if (user != null && user.Designation != null)
                    {
                        model.Allocations.AllocationOwnerDesignation = user.Designation.FirstOrDefault().Designation.Name;
                    }
                    _logger.LogDebug("TeleCallerDashBoard : Allocation details fetched");
                }
            }
            if (model.ChequeRejected != null)
            {
                _logger.LogDebug("TeleCallerDashBoard : ChequeRejected details fetched");
                foreach (var cheque in model.ChequeRejected)
                {
                    model.Cheques.Add(cheque);
                }
            }

            var collections = await _repoFactory.GetRepo().FindAll<Collection>().Where(i => i.AccountId == _params.Id)
                           .Select(c => new TelecallerDashboardCollectionsOutputApiModel
                           {
                               Id = c.Id,
                               ReceiptNo = c.CustomId,
                               PaymentDate = c.CollectionDate,
                               Amount = c.Amount,
                               CollectionMode = c.CollectionMode,
                               InstrumentNumber = c.Cheque.InstrumentNo,
                               BankName = c.Cheque.BankName,
                               BranchName = c.Cheque.BranchName,
                               CollectorFirstName = c.Collector.FirstName,
                               CollectorLastName = c.Collector.LastName,
                               PayerImageName = c.PayerImageName,
                               ChangeRequestImageName = c.ChangeRequestImageName,
                               PaymentStatus = (c.CollectionBatch != null) ? (c.CollectionBatch.PayInSlip != null ? c.CollectionBatch.PayInSlip.PayInSlipWorkflowState.Name : c.CollectionWorkflowState.Name) : c.CollectionWorkflowState.Name
                           }).OrderByDescending(z => z.PaymentDate).ToListAsync();

            model.CollectionHistory = new List<TelecallerDashboardCollectionsOutputApiModel>();
            if (collections != null)
            {
                model.CollectionHistory = collections;
            }

            var feedbacks = await _repoFactory.GetRepo().FindAll<Feedback>().Where(i => i.AccountId == _params.Id)

                                       .Select(f => new TelecallerDashboardFeedBacksHistoryOutputApiModel
                                       {
                                           Id = f.Id,
                                           CustomerMet = f.CustomerMet,
                                           DispositionCode = f.DispositionCode,
                                           PTPDate = f.PTPDate,
                                           PTPAmount = f.PTPAmount,
                                           EscalateTo = f.EscalateTo,
                                           IsReallocationRequest = f.IsReallocationRequest,
                                           ReallocationRequestReason = f.ReallocationRequestReason,
                                           NewArea = f.NewArea,
                                           NewAddress = f.NewAddress,
                                           City = f.City,
                                           NewContact = f.NewContactNo,
                                           Remarks = f.Remarks,
                                           CollectorFirstName = f.Collector.FirstName,
                                           CollectorLastName = f.Collector.LastName,
                                           DispositionDate = f.DispositionDate,
                                           UploadedFileName = f.UploadedFileName,
                                           DispositionGroup = f.DispositionGroup,
                                           PickAddress = f.PickAddress,
                                           OtherPickAddress = f.OtherPickAddress,
                                           AgentContactNo = f.CreatedBy,
                                           Place_of_visit = f.Place_of_visit,
                                           ThirdPartyContactPerson = f.ThirdPartyContactPerson,
                                           NonPaymentReason = f.NonPaymentReason,
                                           State = f.State,
                                           AssignReason = f.AssignReason,
                                           ModeOfCommunication=f.ModeOfCommunication,
                                           AssignToFirstName = f.Assignee.FirstName,
                                           AssignToLastName = f.Assignee.LastName
                                       }).OrderByDescending(p => p.DispositionDate).ToListAsync();

            model.FeedBackHistory = new List<TelecallerDashboardFeedBacksHistoryOutputApiModel>();
            if (feedbacks != null)
            {
                List<string> ids = feedbacks.Select(c => c.AgentContactNo).ToList();
                var result = await _repoFactory.GetRepo().FindAll<ApplicationUser>().Where(x => ids.Contains(x.Id)).Select(u => new { u.Id, u.PrimaryMobileNumber }).ToListAsync();
                // Create a dictionary to map IDs to user names
                var userMap = result.ToDictionary(u => u.Id, u => $"{u.PrimaryMobileNumber}");

                foreach (var feedback in feedbacks)
                {
                    if (userMap != null && feedback?.AgentContactNo != null && userMap.TryGetValue(feedback.AgentContactNo, out string mobileNo))
                    {
                        feedback.AgentContactNo = mobileNo;
                    }
                }

                model.FeedBackHistory = feedbacks;
            }

            //LoanAccount Notes

            #region Notes

            var notes = await _repoFactory.GetRepo()
                        .FindAll<LoanAccountNote>()
                        .Where(i => i.LoanAccountId == _params.Id)
                        .GroupJoin(
                            _repoFactory.GetRepo().FindAll<ApplicationUser>(),
                            note => note.CreatedBy,
                            user => user.Id,
                            (note, users) => new { Note = note, User = users.FirstOrDefault() }
                        )
                        .Select(result => new LoanAccountNotesOutputApiModel
                        {
                            Id = result.Note.Id,
                            Code = result.Note.Code,
                            Description = result.Note.Description,
                            UserName = result.User != null ? result.User.FirstName + " " + result.User.LastName : string.Empty,
                            UserCode = result.User != null ? result.User.CustomId : string.Empty,
                            NoteDateTime = result.Note.CreatedDate.DateTime
                        })
                        .OrderByDescending(z => z.NoteDateTime)
                        .ToListAsync();

            model.Notes = notes ?? new List<LoanAccountNotesOutputApiModel>();

            #endregion Notes

            //LoanAccount Flag

            #region Flags

            var flags = await _repoFactory.GetRepo()
                        .FindAll<LoanAccountFlag>()
                        .Where(i => i.LoanAccountId == _params.Id)
                        .GroupJoin(
                            _repoFactory.GetRepo().FindAll<ApplicationUser>(),
                            flag => flag.CreatedBy,
                            user => user.Id,
                            (flag, users) => new { Flag = flag, User = users.FirstOrDefault() }
                            )
                            .Select(result => new LoanAccountFlagsOutputApiModel
                            {
                                Id = result.Flag.Id,
                                Name = result.Flag.Name,
                                IsActive = result.Flag.IsActive,
                                UserName = result.User.FirstName + " " + result.User.LastName,
                                UserCode = result.User.CustomId,
                                FlagDateTime = result.Flag.CreatedDate.DateTime
                            })
                            .OrderByDescending(z => z.FlagDateTime)
                            .ToListAsync();

            model.Flags = flags ?? new List<LoanAccountFlagsOutputApiModel>();

            #endregion Flags

            return model;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override IQueryable<T> Build<T>()
        {
            _repoFactory.Init(_params);
            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>()
                                    .IncludeAgency()
                                    .IncludeTeleCallingAgency()
                                    .IncludeTeleCaller()
                                    .IncludeAllocationOwner()
                                    .IncludeCollector()
                                    .Where(t => t.Id == _params.Id);
            return query;
        }

        public async Task<string> FetchProductCodeByAccountId(string _Id)
        {
            string productCode = string.Empty;
            try
            {
                productCode = await _repoFactory.GetRepo().FindAll<LoanAccount>().Where(x => x.Id == _Id).Select(a => a.ProductCode).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
            return productCode;
        }
    }

    public class GetTeleCallerAccountDetailsParams : DtoBridge
    {
        [RegularExpression("^[a-zA-Z0-9 ]*$", ErrorMessage = "Invalid Id")]
        public string? Id { get; set; }

        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Invalid CustomerName")]
        public string? CustomerName { get; set; }

        [RegularExpression("^[a-zA-Z0-9- ]*$", ErrorMessage = "Invalid AccountNo")]
        public string? AccountNo { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid MobileNumber")]
        public string? MobileNumber { get; set; }

        public string? CustomerID { get; set; }
        public string? ReferenceId { get; set; }
        public bool isloanaccount { get; set; }

        public string? CreditCardNumber { get; set; }
        public string? PartnerLoanId { get; set; }
        public int take { get; set; }
        public int skip { get; set; }
    }
}