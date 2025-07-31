using System.ComponentModel.DataAnnotations;
using System.Text;
using ENCollect.Security;
using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public class SearchAccountsByFilter : FlexiQueryPagedListBridgeAsync<LoanAccount, SearchAccountsByFilterParams, SearchAccountsByFilterDto, FlexAppContextBridge>
    {
        protected readonly ILogger<SearchAccountsByFilter> _logger;
        protected SearchAccountsByFilterParams _params;
        protected readonly IRepoFactory _repoFactory;
        private readonly ICustomUtility _customUtility;
       
        private string key;
        private string CreditCardNumber;
        private int _take;
        private int _skip;
        protected string _userId;
        protected string _parentId;
        protected string scope;
        string agencyId = string.Empty;
        ApplicationUser usertype = new ApplicationUser();
        protected FlexAppContextBridge? _flexAppContext;
        private EffectiveScope _scope;

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
        public SearchAccountsByFilter(ILogger<SearchAccountsByFilter> logger, 
            IRepoFactory repoFactory, 
            ICustomUtility customUtility,
             IAccountabilityQueryRepository accountabilityQueryRepository,
            IAccountScopeConfigurationQueryRepository scopeConfigurationQueryRepository,

            AccountScopeEvaluatorService scopeEvaluatorService)            
        {
            _logger = logger;
            _repoFactory = repoFactory;
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
        public virtual SearchAccountsByFilter AssignParameters(SearchAccountsByFilterParams @params)
        {
            _params = @params;
            _userId = _params.GetAppContext()?.UserId;
            _take = 50;
            _skip = 0;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<FlexiPagedList<SearchAccountsByFilterDto>> Fetch()
        {
            _repoFactory.Init(_params);
            _flexAppContext = _params.GetAppContext();           

            await DecryptCreditCardNumberAsync();

            // Set the account scope for the current user.
            _scope = await GetAccountScope(_userId);

            //Get the user`s parent
            _parentId = await FetchUserParentIdAsync();

            var projection = Build<LoanAccount>().SelectTo<SearchAccountsByFilterDto>().OrderBy(a => a.CollectorAllocationExpiryDate).ToList();
            projection.ForEach(x =>
            {
                x.ExpiresInDays = ((x.CollectorAllocationExpiryDate?.Date ?? DateTime.Now.Date) - DateTime.Now.Date).Days;
                x.AllocationExpiryColor = _customUtility.ReturnExpiryColorBasedOnExpiryDays(x.ExpiresInDays);
            }
          );

            await FetchLoanAccountJsonData(projection);

            var result = BuildPagedOutput(projection);

            return result;
        }

        private async Task FetchLoanAccountJsonData(List<SearchAccountsByFilterDto> projection)
        {
            foreach (SearchAccountsByFilterDto account in projection)
            {
                LoanAccountJSON json;
                Dictionary<dynamic, dynamic> accountData = new Dictionary<dynamic, dynamic>();

                json = await _repoFactory.GetRepo().FindAll<LoanAccountJSON>().Where(x => x.AccountId == account.Id).FirstOrDefaultAsync();
                accountData = JsonConvert.DeserializeObject<Dictionary<dynamic, dynamic>>(json.AccountJSON);

                account.AccountStatus = _customUtility.GetValue(accountData, LoanAccountJsonEnum.Status.Value);
                account.PDD = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PaymentDueDate.Value);
                account.CurrentBalance = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CurrentBalance.Value);
            }
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
        private async Task DecryptCreditCardNumberAsync()
        {
            if (!string.IsNullOrEmpty(_params.ReferenceID))
            {
                var userloginresult = await _repoFactory.GetRepo().Find<UserLoginKeys>(_params.ReferenceID).FirstOrDefaultAsync();
                key = userloginresult != null ? userloginresult.Key : "";
                var aesGcmCrypto = new AesGcmCrypto();
                var aesGcmKey = Encoding.UTF8.GetBytes(key);
                CreditCardNumber = !string.IsNullOrEmpty(_params.CardNumber) ? aesGcmCrypto.Decrypt(_params.CardNumber, aesGcmKey) : string.Empty;
                _params.AccountNumber = !string.IsNullOrEmpty(_params.AccountNumber) ? aesGcmCrypto.Decrypt(_params.AccountNumber, aesGcmKey) : string.Empty;
            }
        }
        private async Task<string> FetchUserParentIdAsync()
        {
            string? parentId = _scope.ParentId;
            if (string.IsNullOrEmpty(_scope.ParentId))
            {
                // Fetch the user once and check their type
                var user = await _repoFactory.GetRepo().FindAll<ApplicationUser>().FirstOrDefaultAsync(a => a.Id == _userId);

                switch (user)
                {
                    case AgencyUser agencyUser:
                        parentId = agencyUser.AgencyId;
                        break;

                    case CompanyUser companyUser:
                        parentId = companyUser.BaseBranchId;
                        break;

                    default:
                        parentId = null; // Ensure _parentId is reset if user is not found
                        break;
                }
            }
            return parentId;
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
                                         .IncludeAccountJson()
                                         .ByAccountNo(_params.AccountNumber)
                                         .ByLastXDigitsOfAccountNo(_params.LastXDigitsOfAccountNo)
                                         .ByPartnerLoanId(_params.PartnerLoanId)
                                         .ByCustomerName(_params.Name)
                                         .ByMobileNumber(_params.Mobile)
                                         .ByCustomerId(_params.CustomerID)
                                         .ByCentreID(_params.centreID)
                                         .ByGroupID(_params.groupID)
                                         .ByCardNumber(CreditCardNumber)
                                         .ByLastXDigitsOfCardNumber(_params.LastXDigitsOfCreditCardNumber)
                                         .ByCustomerReferenceNumber(_params.CustomerReferenceNumber)
                                         .byCity(_params.City)
                                         .byBranch(_params.Branch)
                                         .byBucket(_params.Bucket)
                                         .byBillingCycle(_params.Cycle)
                                         .byBranchCode(_params.BranchCode)
                                         .byPaymentStatus(_params.Status)
                                         .ByUserType(usertype,_parentId)
                                         .ByScope(_scope, _userId);

            if ((!string.IsNullOrEmpty(_params.flag) && string.Equals(_params.flag.Replace(" ", ""), "creditcard", StringComparison.OrdinalIgnoreCase))
                    || !_params.isloanaccount)
            {
                query = query.ByCCAccount();
            }

            query = CreatePagedQuery<T>(query, _params.PageNumber, _params.Skip, _params.PageSize);

            return query;
        }
        
    }

    /// <summary>
    ///
    /// </summary>
    public class SearchAccountsByFilterParams : PagedQueryParamsDtoBridge
    {
        public string? AccountNumber { get; set; }
        public string? LastXDigitsOfAccountNo { get; set; }

        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Invalid Area")]
        public string? Area { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid Mobile")]
        public string? Mobile { get; set; }

        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Invalid Name")]
        public string? Name { get; set; }

        [RegularExpression("^[a-zA-Z0-9 ]*$", ErrorMessage = "Invalid CustomerID")]
        public string? CustomerID { get; set; }

        public string? centreID { get; set; }
        public string? groupID { get; set; }
        public string? ReferenceID { get; set; }
        public string? CardNumber { get; set; }
        public string? LastXDigitsOfCreditCardNumber { get; set; }
        public string? CustomerReferenceNumber { get; set; }
        public int? RiskProfiling { get; set; }
        public string? City { get; set; }
        public string? Branch { get; set; }
        public string? Bucket { get; set; }
        public string? flag { get; set; }
        public string? PartnerLoanId { get; set; }
        public string? Cycle { get; set; }
        public string? BranchCode { get; set; }
        public string? Status { get; set; }
        public bool isloanaccount { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
    }
}