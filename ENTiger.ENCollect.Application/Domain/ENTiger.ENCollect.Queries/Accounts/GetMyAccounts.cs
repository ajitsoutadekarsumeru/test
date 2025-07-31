using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetMyAccounts : FlexiQueryPagedListBridgeAsync<LoanAccount, GetMyAccountsParams, GetMyAccountsDto, FlexAppContextBridge>
    {
        protected readonly ILogger<GetMyAccounts> _logger;
        protected GetMyAccountsParams _params;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        private string userId = string.Empty;
        private readonly ICustomUtility _utility;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetMyAccounts(ILogger<GetMyAccounts> logger, IRepoFactory repoFactory, ICustomUtility utility)
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
        public virtual GetMyAccounts AssignParameters(GetMyAccountsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<FlexiPagedList<GetMyAccountsDto>> Fetch()
        {
            _repoFactory.Init(_params);
            _flexAppContext = _params.GetAppContext();  //do not remove this line
            userId = _flexAppContext.UserId;

            var projection = await Build<LoanAccount>().SelectTo<GetMyAccountsDto>().OrderBy(a => a.CollectorAllocationExpiryDate).ToListAsync();
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
            _repoFactory.Init(_params);

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().BySecondaryAllocation(userId);

            //Build Your Query With All Parameters Here

            query = CreatePagedQuery<T>(query, _params.PageNumber, _params.Skip, _params.PageSize);

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetMyAccountsParams : PagedQueryParamsDtoBridge
    {
    }
}