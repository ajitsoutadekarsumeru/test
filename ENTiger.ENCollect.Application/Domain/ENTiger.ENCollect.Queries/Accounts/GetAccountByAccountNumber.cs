using ENCollect.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sumeru.Flex;
using System.Text;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetAccountByAccountNumber : FlexiQueryBridgeAsync<LoanAccount, GetAccountByAccountNumberDto>
    {
        protected readonly ILogger<GetAccountByAccountNumber> _logger;
        protected GetAccountByAccountNumberParams _params;
        protected readonly IRepoFactory _repoFactory;
        private readonly ICustomUtility _customUtility;
        private readonly IRoleSearchScopeUtility _roleSearchScopeUtility;
        protected string _userId;
        protected string _parentId;
        private ApplicationUser usertype = new ApplicationUser();
        private EffectiveScope _scope;
        private readonly EncryptionSettings _encryptionSettings;

        private readonly IAccountabilityQueryRepository _accountabilityQueryRepository;
        private readonly IAccountScopeConfigurationQueryRepository _scopeConfigurationQueryRepository;
        
        private readonly AccountScopeEvaluatorService _scopeEvaluatorService;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetAccountByAccountNumber(ILogger<GetAccountByAccountNumber> logger, 
            IRepoFactory repoFactory, 
            IOptions<EncryptionSettings> encryptionSettings, 
            ICustomUtility customUtility,
             IAccountabilityQueryRepository accountabilityQueryRepository,
            IAccountScopeConfigurationQueryRepository scopeConfigurationQueryRepository,
            
            AccountScopeEvaluatorService scopeEvaluatorService
            )
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _encryptionSettings = encryptionSettings.Value;
            _customUtility = customUtility;
            _accountabilityQueryRepository = accountabilityQueryRepository;
            _scopeConfigurationQueryRepository = scopeConfigurationQueryRepository;

            _scopeEvaluatorService = scopeEvaluatorService;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetAccountByAccountNumber AssignParameters(GetAccountByAccountNumberParams @params)
        {
            _params = @params;
            _userId = _params.GetAppContext()?.UserId;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<GetAccountByAccountNumberDto> Fetch()
        {
            _repoFactory.Init(_params);

            // Decrypt the account number from the parameters.
            DecryptAccountNumber();

            // Set the account scope for the current user.
            _scope = await GetAccountScope(_userId);

            var result = await Build<LoanAccount>().FirstOrDefaultAsync();
            if (result == null)
            {
                return null;
            }

            // Map the domain entity into a DTO using a dedicated mapper.
            return await MapToDto(result);
        }

        private async Task<GetAccountByAccountNumberDto> FetchLoanAccountJsonData(AesGcmCrypto aesGcmCrypto, byte[] aesGcmKey, GetAccountByAccountNumberDto model, LoanAccount? result)
        {
            if (result != null)
            {
                Dictionary<dynamic, dynamic> accountData = new Dictionary<dynamic, dynamic>();
                model = new GetAccountByAccountNumberDto();
                LoanAccountJSON json = await _repoFactory.GetRepo().FindAll<LoanAccountJSON>().Where(x => x.AccountId == result.Id.ToString()).FirstOrDefaultAsync();

                accountData = JsonConvert.DeserializeObject<Dictionary<dynamic, dynamic>>(json.AccountJSON);

                model.NextDueDate = result.NEXT_DUE_DATE;
                model.PropensityToPayConfidence = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PropensityToPayConfidence.Value);
                model.PropensityToPayConfidence = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PropensityToPay.Value);
                model.BCC_PENDING = _customUtility.GetValue(accountData, LoanAccountJsonEnum.BCCPending.Value);
                model.GroupName = result.GroupName;
                model.referencE1_ADDRS = _customUtility.GetValue(accountData, LoanAccountJsonEnum.Reference1Address.Value);
                model.physicaL_ADDRESS = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PhysicalAddress.Value);
                model.motherS_ADDRS = _customUtility.GetValue(accountData, LoanAccountJsonEnum.MothersAddress.Value);
                model.fatherS_ADDRS = _customUtility.GetValue(accountData, LoanAccountJsonEnum.FathersAddress.Value);
                model.guarantoR1_CITY = _customUtility.GetValue(accountData, LoanAccountJsonEnum.Guarantor1City.Value);
                model.newAddress = _customUtility.GetValue(accountData, LoanAccountJsonEnum.NewAddress.Value);
                model.BOUNCED_CHEQUE_CHARGES = _customUtility.GetValue(accountData, LoanAccountJsonEnum.BouncedChequeCharges.Value);
                model.Guarantor_Mailing_Address = _customUtility.GetValue(accountData, LoanAccountJsonEnum.Guarantor3.Value);
                model.Partner_Name = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PartnerName.Value);
                model.Partner_Loan_ID = result.Partner_Loan_ID;
                model.Partner_Customer_ID = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PartnerCustomerID.Value);
                model.BounceCharges = _customUtility.GetValue(accountData, LoanAccountJsonEnum.TotalBounceCharge.Value);
                model.TOTAL_OVERDUE_AMT = _customUtility.GetValue(accountData, LoanAccountJsonEnum.TotalOverdueAmount.Value);
                model.TOTAL_LATE_PAYMENT_CHARGE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.TotalLatePaymentCharge.Value);
                model.AssetType = _customUtility.GetValue(accountData, LoanAccountJsonEnum.AssetType.Value);
                model.AssetDetails = _customUtility.GetValue(accountData, LoanAccountJsonEnum.AssetDetails.Value);

                model.IsEligibleForRepossession = result.IsEligibleForRepossession;
                model.IsEligibleForRestructure = result.IsEligibleForRestructure;
                model.IsEligibleForSettlement = result.IsEligibleForSettlement;
                model.IsEligibleForLegal = result.IsEligibleForLegal;
                model.TOS = Convert.ToDecimal(string.IsNullOrEmpty(result.TOS) ? "0" : result.TOS);
                model.EMIAmount = result.EMIAMT;
                model.CurrentBucket = result.CURRENT_BUCKET;
                model.MonthStartingBucket = result.BUCKET != null ? Convert.ToString(result.BUCKET) : string.Empty;
                model.POS = result.BOM_POS;
                model.ProductName = result.PRODUCT;
                model.ProductGroup = result.ProductGroup;
                model.City = result.CITY;
                model.State = result.STATE;
                model.CurrentDPD = result.CURRENT_DPD != null ? Convert.ToString(result.CURRENT_DPD) : string.Empty;
                model.Id = result.Id;
                model.PRINCIPLE_OVERDUE = result.PRINCIPAL_OD;
                model.INTEREST_OVERDUE = result.INTEREST_OD;
                model.PTPAmount = result.LatestPTPAmount;
                model.LatestPTPAmount = result.LatestPTPAmount;
                model.PENAL_PENDING = result.PENAL_PENDING;
                model.CURRENT_POS = result.CURRENT_POS;
                model.EMI_OD_AMT = result.EMI_OD_AMT;
                model.PTPDate = result.LatestPTPDate;
                model.Branch = result.BRANCH;
                model.AccountCategory = result.SCHEME_DESC;
                model.SubProductName = result.SubProduct;
                model.BranchCode = result.BranchCode;
                model.CentreID = result.CentreID;
                model.CentreName = result.CentreName;
                model.GroupID = result.GroupId;
                model.Partner_Loan_ID = result.Partner_Loan_ID;

                model.DPD = result.OVERDUE_DAYS;
                model.Tenure = _customUtility.GetValue(accountData, LoanAccountJsonEnum.TenureDays.Value);
                model.RepaymentMode = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PreferredModeOfPayment.Value);
                model.BounceChargesDue = _customUtility.GetValue(accountData, LoanAccountJsonEnum.TotalBounceCharge.Value);
                model.BounceReason = _customUtility.GetValue(accountData, LoanAccountJsonEnum.LastChequeBounceReason.Value);
                model.Current_POS = result.CURRENT_POS;
                model.WOFFFlag = _customUtility.GetValue(accountData, LoanAccountJsonEnum.RephasementFlag.Value);
                model.PaymentStatus = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PaymentStatus.Value);
                model.BounceChargeOverdue = _customUtility.GetValue(accountData, LoanAccountJsonEnum.LastChequeBounceAmount.Value);
                model.ResidentialAddress = _customUtility.GetValue(accountData, LoanAccountJsonEnum.MailingAddress.Value);
                model.ResidentialNumber = result.MAILINGMOBILE;
                model.OfficeAddress = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CustomerOfficePinCode.Value);
                model.OfficeNumber = _customUtility.GetValue(accountData, LoanAccountJsonEnum.NonMailingPhone1.Value);
                model.CoApplicantName = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CoApplicant1.Value);
                model.CoApplicantNumber = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CoApplicant1Contact.Value);
                model.OTHER_CHARGES = result.OTHER_CHARGES;
                model.NO_OF_EMI_OD = result.NO_OF_EMI_OD;
                model.Address = _customUtility.GetValue(accountData, LoanAccountJsonEnum.MailingAddress.Value);
                model.Latest_Address_From_Trail = result.Latest_Address_From_Trail;
                model.CustomerId = result.CUSTOMERID;

                model.AccountNo = aesGcmCrypto.Encrypt(result.AGREEMENTID ?? "", aesGcmKey);
                model.CreditCardNo = aesGcmCrypto.Encrypt(result.PRIMARY_CARD_NUMBER ?? "", aesGcmKey);
                model.CustomerName = aesGcmCrypto.Encrypt(result.CUSTOMERNAME ?? "", aesGcmKey);
                model.EMailId = aesGcmCrypto.Encrypt(result.LatestEmailId ?? "", aesGcmKey);
                model.MobileNo = aesGcmCrypto.Encrypt(result.LatestMobileNo ?? "", aesGcmKey);

                model.Latest_Email_From_Trail = aesGcmCrypto.Encrypt(result.Latest_Email_From_Trail ?? "", aesGcmKey);
                model.Latest_Number_From_Trail = aesGcmCrypto.Encrypt(result.Latest_Number_From_Trail ?? "", aesGcmKey);
                model.Latest_Number_From_Receipt = aesGcmCrypto.Encrypt(result.Latest_Number_From_Receipt ?? "", aesGcmKey);
                model.Latest_Number_From_Send_Payment = aesGcmCrypto.Encrypt(result.Latest_Number_From_Send_Payment ?? "", aesGcmKey);
            }

            return model;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override IQueryable<T> Build<T>()
        {
            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>()
                .ByAccountNo(_params.AccountNo)
                .ByScope(_scope, _userId);

           
            return query;
        }
        private async Task FetchUserParentIdAsync()
        {
            // Fetch the user once and check their type
            var user = await _repoFactory.GetRepo().FindAll<ApplicationUser>()
                        .FirstOrDefaultAsync(a => a.Id == _userId);

            switch (user)
            {
                case AgencyUser agencyUser:
                    _parentId = agencyUser.AgencyId;
                    break;

                case CompanyUser companyUser:
                    _parentId = companyUser.BaseBranchId;
                    break;

                default:
                    _parentId = null; // Ensure _parentId is reset if user is not found
                    break;
            }
        }
        private void DecryptAccountNumber()
        {
            string encryptionKey = _encryptionSettings.StaticKeys.DecryptionKey;
            var aesGcmKey = Encoding.UTF8.GetBytes(encryptionKey);
            var aesGcmCrypto = new AesGcmCrypto();
            _params.AccountNo = aesGcmCrypto.Decrypt(_params.AccountNo, aesGcmKey);
        }
        private async Task<GetAccountByAccountNumberDto> MapToDto(LoanAccount loanAccount)
        {
            string encryptionKey = _encryptionSettings.StaticKeys.DecryptionKey;
            var aesGcmKey = Encoding.UTF8.GetBytes(encryptionKey);
            var aesGcmCrypto = new AesGcmCrypto();

            // Retrieve and deserialize JSON account data.
            var loanAccountJson = await _repoFactory.GetRepo()
                                                    .FindAll<LoanAccountJSON>()
                                                    .Where(x => x.AccountId == loanAccount.Id)
                                                    .FirstOrDefaultAsync();
            var accountData = JsonConvert.DeserializeObject<Dictionary<dynamic, dynamic>>(loanAccountJson.AccountJSON);

            var dto = new GetAccountByAccountNumberDto();

            // Map domain properties.
            dto.NextDueDate = loanAccount.NEXT_DUE_DATE;
            dto.GroupName = loanAccount.GroupName;
            dto.Partner_Loan_ID = loanAccount.Partner_Loan_ID;
            dto.CURRENT_POS = loanAccount.CURRENT_POS;
            dto.POS = loanAccount.BOM_POS;
            dto.ProductName = loanAccount.PRODUCT;
            dto.ProductGroup = loanAccount.ProductGroup;
            dto.City = loanAccount.CITY;
            dto.State = loanAccount.STATE;
            dto.Id = loanAccount.Id;
            dto.PRINCIPLE_OVERDUE = loanAccount.PRINCIPAL_OD;
            dto.INTEREST_OVERDUE = loanAccount.INTEREST_OD;
            dto.PTPAmount = loanAccount.LatestPTPAmount;
            dto.LatestPTPAmount = loanAccount.LatestPTPAmount;
            dto.PENAL_PENDING = loanAccount.PENAL_PENDING;
            dto.EMI_OD_AMT = loanAccount.EMI_OD_AMT;
            dto.PTPDate = loanAccount.LatestPTPDate;
            dto.Branch = loanAccount.BRANCH;
            dto.AccountCategory = loanAccount.SCHEME_DESC;
            dto.SubProductName = loanAccount.SubProduct;
            dto.BranchCode = loanAccount.BranchCode;
            dto.CentreID = loanAccount.CentreID;
            dto.CentreName = loanAccount.CentreName;
            dto.GroupID = loanAccount.GroupId;
            dto.DPD = loanAccount.OVERDUE_DAYS;
            dto.ResidentialNumber = loanAccount.MAILINGMOBILE;
            dto.OTHER_CHARGES = loanAccount.OTHER_CHARGES;
            dto.NO_OF_EMI_OD = loanAccount.NO_OF_EMI_OD;
            dto.Latest_Address_From_Trail = loanAccount.Latest_Address_From_Trail;
            dto.CustomerId = loanAccount.CUSTOMERID;
            dto.CurrentBucket = loanAccount.CURRENT_BUCKET;
            dto.MonthStartingBucket = loanAccount.BUCKET != null ? Convert.ToString(loanAccount.BUCKET) : string.Empty;
            dto.CurrentDPD = loanAccount.CURRENT_DPD != null ? Convert.ToString(loanAccount.CURRENT_DPD) : string.Empty;
            dto.TOS = Convert.ToDecimal(string.IsNullOrEmpty(loanAccount.TOS) ? "0" : loanAccount.TOS);
            dto.EMIAmount = loanAccount.EMIAMT;

            // Map values from the JSON account data using the custom utility.
            dto.PropensityToPayConfidence = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PropensityToPayConfidence.Value);
            // NOTE: Original code set this property twice. Ensure the correct mapping for each property.
            dto.PropensityToPay = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PropensityToPay.Value);
            dto.BCC_PENDING = _customUtility.GetValue(accountData, LoanAccountJsonEnum.BCCPending.Value);
            dto.referencE1_ADDRS = _customUtility.GetValue(accountData, LoanAccountJsonEnum.Reference1Address.Value);
            dto.physicaL_ADDRESS = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PhysicalAddress.Value);
            dto.motherS_ADDRS = _customUtility.GetValue(accountData, LoanAccountJsonEnum.MothersAddress.Value);
            dto.fatherS_ADDRS = _customUtility.GetValue(accountData, LoanAccountJsonEnum.FathersAddress.Value);
            dto.guarantoR1_CITY = _customUtility.GetValue(accountData, LoanAccountJsonEnum.Guarantor1City.Value);
            dto.newAddress = _customUtility.GetValue(accountData, LoanAccountJsonEnum.NewAddress.Value);
            dto.BOUNCED_CHEQUE_CHARGES = _customUtility.GetValue(accountData, LoanAccountJsonEnum.BouncedChequeCharges.Value);
            dto.Guarantor_Mailing_Address = _customUtility.GetValue(accountData, LoanAccountJsonEnum.Guarantor3.Value);
            dto.Partner_Customer_ID = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PartnerCustomerID.Value);
            dto.BounceCharges = _customUtility.GetValue(accountData, LoanAccountJsonEnum.TotalBounceCharge.Value);
            dto.TOTAL_OVERDUE_AMT = _customUtility.GetValue(accountData, LoanAccountJsonEnum.TotalOverdueAmount.Value);
            dto.TOTAL_LATE_PAYMENT_CHARGE = _customUtility.GetValue(accountData, LoanAccountJsonEnum.TotalLatePaymentCharge.Value);
            dto.AssetType = _customUtility.GetValue(accountData, LoanAccountJsonEnum.AssetType.Value);
            dto.AssetDetails = _customUtility.GetValue(accountData, LoanAccountJsonEnum.AssetDetails.Value);
            dto.Tenure = _customUtility.GetValue(accountData, LoanAccountJsonEnum.TenureDays.Value);
            dto.RepaymentMode = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PreferredModeOfPayment.Value);
            dto.BounceChargesDue = _customUtility.GetValue(accountData, LoanAccountJsonEnum.TotalBounceCharge.Value);
            dto.BounceReason = _customUtility.GetValue(accountData, LoanAccountJsonEnum.LastChequeBounceReason.Value);
            dto.WOFFFlag = _customUtility.GetValue(accountData, LoanAccountJsonEnum.RephasementFlag.Value);
            dto.PaymentStatus = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PaymentStatus.Value);
            dto.BounceChargeOverdue = _customUtility.GetValue(accountData, LoanAccountJsonEnum.LastChequeBounceAmount.Value);
            dto.ResidentialAddress = _customUtility.GetValue(accountData, LoanAccountJsonEnum.MailingAddress.Value);
            dto.OfficeAddress = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CustomerOfficePinCode.Value);
            dto.OfficeNumber = _customUtility.GetValue(accountData, LoanAccountJsonEnum.NonMailingPhone1.Value);
            dto.CoApplicantName = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CoApplicant1.Value);
            dto.CoApplicantNumber = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CoApplicant1Contact.Value);

            // Set eligibility flags.
            dto.IsEligibleForRepossession = loanAccount.IsEligibleForRepossession;
            dto.IsEligibleForRestructure = loanAccount.IsEligibleForRestructure;
            dto.IsEligibleForSettlement = loanAccount.IsEligibleForSettlement;
            dto.IsEligibleForLegal = loanAccount.IsEligibleForLegal;

            // Encrypt sensitive properties.
            dto.AccountNo = aesGcmCrypto.Encrypt(loanAccount.AGREEMENTID ?? string.Empty, aesGcmKey);
            dto.CreditCardNo = aesGcmCrypto.Encrypt(loanAccount.PRIMARY_CARD_NUMBER ?? string.Empty, aesGcmKey);
            dto.CustomerName = aesGcmCrypto.Encrypt(loanAccount.CUSTOMERNAME ?? string.Empty, aesGcmKey);
            dto.EMailId = aesGcmCrypto.Encrypt(loanAccount.LatestEmailId ?? string.Empty, aesGcmKey);
            dto.MobileNo = aesGcmCrypto.Encrypt(loanAccount.LatestMobileNo ?? string.Empty, aesGcmKey);
            dto.Latest_Email_From_Trail = aesGcmCrypto.Encrypt(loanAccount.Latest_Email_From_Trail ?? string.Empty, aesGcmKey);
            dto.Latest_Number_From_Trail = aesGcmCrypto.Encrypt(loanAccount.Latest_Number_From_Trail ?? string.Empty, aesGcmKey);
            dto.Latest_Number_From_Receipt = aesGcmCrypto.Encrypt(loanAccount.Latest_Number_From_Receipt ?? string.Empty, aesGcmKey);
            dto.Latest_Number_From_Send_Payment = aesGcmCrypto.Encrypt(loanAccount.Latest_Number_From_Send_Payment ?? string.Empty, aesGcmKey);
            dto.AccountJSON = loanAccountJson?.AccountJSON ?? null;
            return dto;
        }
        private async Task<EffectiveScope> GetAccountScope(string userId)
        {
            // 1. Fetch Accountabilities for the given user.
            List<Accountability> accountabilities = await _accountabilityQueryRepository.GetAccountabilities(userId, _params.GetAppContext());

            // 2. Fetch Scope Configurations based on the retrieved accountabilities.
            List<AccountScopeConfiguration> scopeConfigs = await _scopeConfigurationQueryRepository.GetScopeConfigurations(accountabilities, _params.GetAppContext());

            // 3. Evaluate the effective scope (which may include the parent's id) using the provided accountabilities and scope configurations.
            EffectiveScope effectiveScope = await _scopeEvaluatorService.EvaluateScope(accountabilities, scopeConfigs, userId, _params.GetAppContext());

            return effectiveScope;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetAccountByAccountNumberParams : DtoBridge
    {
        public string AccountNo { get; set; }
    }
}