using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetByCustomerId : FlexiQueryEnumerableBridgeAsync<LoanAccount, GetByCustomerIdDto>
    {
        protected readonly ILogger<GetByCustomerId> _logger;
        protected GetByCustomerIdParams _params;
        protected readonly IRepoFactory _repoFactory;
        private EffectiveScope _scope;
        private readonly IAccountabilityQueryRepository _accountabilityQueryRepository;
        private readonly IAccountScopeConfigurationQueryRepository _scopeConfigurationQueryRepository;
        private ApplicationUser usertype = new ApplicationUser();
        private readonly AccountScopeEvaluatorService _scopeEvaluatorService;

        protected string _userId;
        protected string _parentId;
        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetByCustomerId(ILogger<GetByCustomerId> logger,
            IRepoFactory repoFactory,
              IAccountabilityQueryRepository accountabilityQueryRepository,
            IAccountScopeConfigurationQueryRepository scopeConfigurationQueryRepository,
            AccountScopeEvaluatorService scopeEvaluatorService)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _accountabilityQueryRepository = accountabilityQueryRepository;
            _scopeConfigurationQueryRepository = scopeConfigurationQueryRepository;
            _scopeEvaluatorService = scopeEvaluatorService;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetByCustomerId AssignParameters(GetByCustomerIdParams @params)
        {
            _params = @params;
            _userId = _params.GetAppContext()?.UserId;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetByCustomerIdDto>> Fetch()
        {
            _repoFactory.Init(_params);
            // Set the account scope for the current user.
            _scope = await GetAccountScope(_userId);

            //Get the user`s parent
            _parentId = await FetchUserParentIdAsync();

            var result = await Build<LoanAccount>().SelectTo<GetByCustomerIdDto>().ToListAsync();

            return result;
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
                                                       .ByCustomerID(_params.customerID)
                                                       .ByUserType(usertype, _parentId)
                                                       .ByScope(_scope, _userId);

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetByCustomerIdParams : DtoBridge
    {
        public string customerID { get; set; }
    }
}