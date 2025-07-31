using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetAttemptedMyAccounts : FlexiQueryPagedListBridgeAsync<LoanAccount, GetAttemptedMyAccountsParams, GetAttemptedMyAccountsDto, FlexAppContextBridge>
    {
        protected readonly ILogger<GetAttemptedMyAccounts> _logger;
        protected GetAttemptedMyAccountsParams _params;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        private readonly ICustomUtility _utility;

        string userId;
        string userType;
        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetAttemptedMyAccounts(ILogger<GetAttemptedMyAccounts> logger, IRepoFactory repoFactory, ICustomUtility utility)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _utility = utility;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetAttemptedMyAccounts AssignParameters(GetAttemptedMyAccountsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<FlexiPagedList<GetAttemptedMyAccountsDto>> Fetch()
        {
            _repoFactory.Init(_params);
            _flexAppContext = _params.GetAppContext();  //do not remove this line
            userId = _flexAppContext.UserId;

            ApplicationUser? user = await _repoFactory.GetRepo().FindAll<ApplicationUser>().Where(a => a.Id == userId).FirstOrDefaultAsync();
            userType = await fetchLoggedInUserTypeAsync(user);

            var projection = await Build<LoanAccount>().SelectTo<GetAttemptedMyAccountsDto>().OrderBy(a => a.CollectorAllocationExpiryDate).ToListAsync();
            projection.ForEach(x =>
            {
                x.ExpiresInDays = ((x.CollectorAllocationExpiryDate?.Date ?? DateTime.Now.Date) - DateTime.Now.Date).Days;
                x.AllocationExpiryColor = _utility.ReturnExpiryColorBasedOnExpiryDays(x.ExpiresInDays);
            }
            );
            var result = BuildPagedOutput(projection);

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override IQueryable<T> Build<T>()
        {
            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().ByMyAccountAllocations(userType, userId).Where(x => x.Attempted == true && x.Paid == false);

            query = CreatePagedQuery<T>(query, _params.PageNumber, _params.Skip, _params.PageSize);

            return query;
        }

        public async Task<string> fetchLoggedInUserTypeAsync(ApplicationUser? loggedinUser)
        {
            if (loggedinUser?.GetType() == typeof(AgencyUser))
            {
                AgencyUser? user = await _repoFactory.GetRepo().FindAll<AgencyUser>().FirstOrDefaultAsync(x => x.Id == loggedinUser.Id);

                if (user?.Designation != null)
                {
                    bool hasTeleCallerDesignation = user.Designation
                                                 .Any(d => d.Designation.Name
                                                 .Equals("tele-caller", StringComparison.OrdinalIgnoreCase));

                    return hasTeleCallerDesignation ? "AgencyUser" : "CompanyUser";
                }
            }
            return "CompanyUser";
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetAttemptedMyAccountsParams : PagedQueryParamsDtoBridge
    {
        public string? accountsType { get; set; }
    }
}