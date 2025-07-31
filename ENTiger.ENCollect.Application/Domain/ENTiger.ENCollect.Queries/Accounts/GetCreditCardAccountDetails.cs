using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetCreditCardAccountDetails : FlexiQueryBridgeAsync<LoanAccount, GetCreditCardAccountDetailsDto>
    {
        protected readonly ILogger<GetCreditCardAccountDetails> _logger;
        protected GetCreditCardAccountDetailsParams _params;
        protected readonly IRepoFactory _repoFactory;
        private readonly ICustomUtility _customUtility;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetCreditCardAccountDetails(ILogger<GetCreditCardAccountDetails> logger, IRepoFactory repoFactory, ICustomUtility customUtility)
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
        public virtual GetCreditCardAccountDetails AssignParameters(GetCreditCardAccountDetailsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<GetCreditCardAccountDetailsDto> Fetch()
        {
            GetCreditCardAccountDetailsDto model = new GetCreditCardAccountDetailsDto();
            Dictionary<dynamic, dynamic> accountData = new Dictionary<dynamic, dynamic>();
            LoanAccountJSON json = new LoanAccountJSON();

            var account = await Build<LoanAccount>().ToListAsync();
            if (account != null)
            {
                json = await _repoFactory.GetRepo().FindAll<LoanAccountJSON>().Where(x => x.AccountId == account.FirstOrDefault().Id).FirstOrDefaultAsync();

                accountData = JsonConvert.DeserializeObject<Dictionary<dynamic, dynamic>>(json.AccountJSON);
            }
            model = account
                        .Select(a => new GetCreditCardAccountDetailsDto
                        {
                            Account = new CreditCardAccountOutputApiModel
                            {
                                AccountJSON = json?.AccountJSON ?? null,
                                CaseDetails = new CCCaseDetailsOutputApiModel
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
                                    LAST_PAYMENT_DATE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.LastPaymentDate.Value),
                                    DueDate = a.DueDate,
                                    WRITEOFFDATE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.WriteOffDate.Value),
                                    ECS_ENABLED = _customUtility.GetValue(accountData, LoanAccountJsonEnum.ECSEnabled.Value),
                                    BILLING_CYCLE = a.BILLING_CYCLE,
                                    CARD_OPEN_DATE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CardOpenDate.Value),
                                    CASH_LIMIT = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CashLimit.Value),
                                    COLLECTIONS_CREATION_DATE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CollectionsCreationDate.Value),
                                    CREDIT_LIMIT = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CreditLimit.Value),
                                    CURRENT_BALANCE_AMOUNT = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CurrentBalanceAmount.Value),
                                    CURRENT_BLOCK_CODE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CurrentBlockCode.Value),
                                    CURRENT_BLOCK_CODE_DATE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CurrentBlockCodeDate.Value),
                                    LAST_PAYMENT_MODE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.LastPaymentMode.Value),
                                    LAST_PAYMENT_REFERENCE_NUMBER = _customUtility.GetValue(accountData, LoanAccountJsonEnum.LastPaymentReferenceNumber.Value),
                                    LAST_PAYMENT_REMARK = _customUtility.GetValue(accountData, LoanAccountJsonEnum.LastPaymentRemark.Value),
                                    LOGO = _customUtility.GetValue(accountData, LoanAccountJsonEnum.Logo.Value),
                                    LOGO_DESCRIPTION = _customUtility.GetValue(accountData, LoanAccountJsonEnum.LogoDescription.Value),
                                    PRIMARY_CARD_NUMBER = a.PRIMARY_CARD_NUMBER,
                                    STATEMENTED_BLOCK_CODE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.StatementedBlockCode.Value),
                                    STATEMENTED_DUE_DATE_PRINTED = _customUtility.GetValue(accountData, LoanAccountJsonEnum.StatementedDueDatePrinted.Value),
                                    STATEMENTED_DUE_DATE_SYSTEM = _customUtility.GetValue(accountData, LoanAccountJsonEnum.StatementedDueDateSystem.Value),
                                    STATEMENTED_OPENING_BALANCE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.StatementedOpeningBalance.Value),
                                    IsEligibleForSettlement = a.IsEligibleForSettlement,
                                    IsEligibleForLegal = a.IsEligibleForLegal,
                                    IsEligibleForRepossession = a.IsEligibleForRepossession,
                                    IsEligibleForRestructure = a.IsEligibleForRestructure,
                                    RephasementFlag = _customUtility.GetValue(accountData, LoanAccountJsonEnum.RephasementFlag.Value)
                                },
                                Analytics = new CCAnalyticsOutputAPIModel
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
                                },
                                CardFinancials = new CCFinancialsOutputApiModel
                                {
                                    LAST_PAYMENT_AMOUNT = _customUtility.GetValue(accountData, LoanAccountJsonEnum.LastPaymentAmount.Value),
                                    CURRENT_DAYS_PAYMENT_DUE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CurrentDaysPaymentDue.Value),
                                    CURRENT_MINIMUM_AMOUNT_DUE = a.CURRENT_MINIMUM_AMOUNT_DUE,
                                    CURRENT_TOTAL_AMOUNT_DUE = a.CURRENT_TOTAL_AMOUNT_DUE,
                                    OVERLIMIT_AMOUNT = _customUtility.GetValue(accountData, LoanAccountJsonEnum.OverlimitAmount.Value),
                                    PAST_DAYS_PAYMENT_DUE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PastDaysPaymentDue.Value),
                                    PAYMENT_CYCLE_DUE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PaymentCycleDue.Value),
                                    PAYMENT_DUE_120_DAYS = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PaymentDue120Days.Value),
                                    PAYMENT_DUE_150_DAYS = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PaymentDue150Days.Value),
                                    PAYMENT_DUE_180_DAYS = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PaymentDue180Days.Value),
                                    PAYMENT_DUE_210_DAYS = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PaymentDue210Days.Value),
                                    PAYMENT_DUE_30_DAYS = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PaymentDue30Days.Value),
                                    PAYMENT_DUE_60_DAYS = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PaymentDue60Days.Value),
                                    PAYMENT_DUE_90_DAYS = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PaymentDue90Days.Value),
                                    WRITE_OFF_AMOUNT = _customUtility.GetValue(accountData, LoanAccountJsonEnum.WriteOffAmount.Value),
                                },
                                CaseStatus = new CCStatusOutputApiModel
                                {
                                    OVERDUE_DAYS = a.OVERDUE_DAYS,
                                    CURRENT_DPD = a.CURRENT_DPD,
                                    CURRENT_BUCKET = a.CURRENT_BUCKET,
                                    BUCKET = a.BUCKET,
                                    NPA_STAGEID = a.NPA_STAGEID,
                                    PAYMENTSTATUS = a.PAYMENTSTATUS,
                                    Month_on_Books = _customUtility.GetValue(accountData, LoanAccountJsonEnum.MonthOnBooks.Value),
                                    NO_OF_EMI_OD = a.NO_OF_EMI_OD
                                },
                                DemographicDetails = new CCDemogDetailsOutputApiModel
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
                                    ADD_ON_CARD_NUMBER = _customUtility.GetValue(accountData, LoanAccountJsonEnum.AddOnCardNumber.Value),
                                    ADD_ON_DESIGNATION = _customUtility.GetValue(accountData, LoanAccountJsonEnum.AddOnDesignation.Value),
                                    ADD_ON_FIRST_NAME = _customUtility.GetValue(accountData, LoanAccountJsonEnum.AddOnFirstName.Value),
                                    ADD_ON_LAST_NAME = _customUtility.GetValue(accountData, LoanAccountJsonEnum.AddOnLastName.Value),
                                    ADD_ON_PERMANENT_CITY = _customUtility.GetValue(accountData, LoanAccountJsonEnum.AddOnPermanentCity.Value),
                                    ADD_ON_PERMANENT_COUNTRY = _customUtility.GetValue(accountData, LoanAccountJsonEnum.AddOnPermanentCountry.Value),
                                    ADD_ON_PERMANENT_POST_CODE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.AddOnPermanentPostCode.Value),
                                    ADD_ON_PERMANENT_STATE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.AddOnPermanentState.Value),
                                    ADD_ON_PERSONAL_EMAIL_ID = _customUtility.GetValue(accountData, LoanAccountJsonEnum.AddOnPersonalEmailId.Value),
                                    ADD_ON_WORK_EMAIL_ID = _customUtility.GetValue(accountData, LoanAccountJsonEnum.AddOnWorkEmailId.Value),
                                    AD_MANDATE_SB_ACC_NUMBER = _customUtility.GetValue(accountData, LoanAccountJsonEnum.AdMandateSbAccNumber.Value),
                                    CUSTOMERS_PERMANENT_EMAIL_ID = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CustomersPermanentEmailId.Value),
                                    NewAddress = _customUtility.GetValue(accountData, LoanAccountJsonEnum.NewAddress.Value),
                                    LatestMobileNo = a.LatestMobileNo
                                },
                                AdditionalDetails = new CCAdditionalDetailsOutputAPIModel
                                {
                                    LAST_CHEQUE_BOUNCE_AMOUNT = _customUtility.GetValue(accountData, LoanAccountJsonEnum.LastChequeBounceAmount.Value),
                                    LAST_CHEQUE_BOUNCE_DATE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.LastChequeBounceDate.Value),
                                    LAST_CHEQUE_BOUNCE_NUMBER = _customUtility.GetValue(accountData, LoanAccountJsonEnum.LastChequeBounceNumber.Value),
                                    LAST_CHEQUE_BOUNCE_REASON = _customUtility.GetValue(accountData, LoanAccountJsonEnum.LastChequeBounceReason.Value),
                                    LAST_CHEQUE_ISSUANCE_BANK_NAME = _customUtility.GetValue(accountData, LoanAccountJsonEnum.LastChequeIssuanceBankName.Value),
                                    LAST_PURCHASED_AMOUNT = _customUtility.GetValue(accountData, LoanAccountJsonEnum.LastPurchasedAmount.Value),
                                    LAST_PURCHASED_DATE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.LastPurchasedDate.Value),
                                    LAST_PURCHASED_TRANSACTION_REMARK = _customUtility.GetValue(accountData, LoanAccountJsonEnum.LastPurchasedTransactionRemark.Value),
                                    TOTAL_PURCHASES = _customUtility.GetValue(accountData, LoanAccountJsonEnum.TotalPurchases.Value),
                                },
                                OtherContactDetails = new CCOtherContactDetailsOutputAPIModel
                                {
                                    PERMANENT_COUNTRY_CODE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PermanentCountryCode.Value),
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
                                    CUSTOMER_EMPLOYER = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CustomerEmployer.Value),
                                    CUSTOMER_OFFICE_COUNTRY = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CustomerOfficeCountry.Value),
                                    CUSTOMER_OFFICE_PIN_CODE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CustomerOfficePinCode.Value),
                                    RESIDENTIAL_COUNTRY = a.RESIDENTIAL_COUNTRY,
                                    RESIDENTIAL_CUSTOMER_CITY = a.RESIDENTIAL_CUSTOMER_CITY,
                                    RESIDENTIAL_CUSTOMER_STATE = a.RESIDENTIAL_CUSTOMER_STATE,
                                    RESIDENTIAL_PIN_CODE = a.RESIDENTIAL_PIN_CODE,
                                },
                                OtherDetails = new CCOtherDetailsOutputAPIModel
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
                                PaymentAndBounceDetails = new CCPaymentAndBounceOutputAPIModel
                                {
                                    LAST_STATEMENT_DATE = a.LAST_STATEMENT_DATE,
                                    LAST_STATEMENT_SENT_AT_ADDRESS_OR_NOT_FLAG = _customUtility.GetValue(accountData, LoanAccountJsonEnum.LastStatementSentAtAddressOrNotFlag.Value),
                                    LAST_STATEMENT_SENT_AT_ADDRESS_TYPE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.LastStatementSentAtAddressType.Value)
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
                            CollectionPoint = new AllocationHistoryOutputApiModel
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
                            }
                        }).FirstOrDefault();

            if (model != null && model.CollectionPoint != null && model.CollectionPoint.AllocationOwnerCode != null)
            {
                var user = await _repoFactory.GetRepo().FindAll<CompanyUser>().Where(x => x.CustomId == model.CollectionPoint.AllocationOwnerCode).FirstOrDefaultAsync();
                if (user != null && user.Designation != null)
                {
                    model.CollectionPoint.AllocationOwnerDesignation = user.Designation.FirstOrDefault().Designation.Name;
                }
            }

            var collections = await _repoFactory.GetRepo().FindAll<Collection>().Where(i => i.AccountId == _params.Id)

                                       .Select(c => new DashboardCollectionsOutputApiModel
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

            model.CollectionHistory = new List<DashboardCollectionsOutputApiModel>();
            if (collections != null)
            {
                model.CollectionHistory = collections;
            }

            var feedbacks = await _repoFactory.GetRepo().FindAll<Feedback>().Where(i => i.AccountId == _params.Id)

                                       .Select(f => new DashboardFeedBacksHistoryOutputApiModel
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
                                           AssignReason = f.AssignReason
                                       }).OrderByDescending(p => p.DispositionDate).ToListAsync();

            model.FeedBackHistory = new List<DashboardFeedBacksHistoryOutputApiModel>();
            if (feedbacks != null)
            {
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
    }

    public class GetCreditCardAccountDetailsParams : DtoBridge
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