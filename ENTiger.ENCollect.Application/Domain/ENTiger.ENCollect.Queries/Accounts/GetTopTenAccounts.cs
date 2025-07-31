using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetTopTenAccounts : FlexiQueryEnumerableBridgeAsync<LoanAccount, GetTopTenAccountsDto>
    {
        protected readonly ILogger<GetTopTenAccounts> _logger;
        protected GetTopTenAccountsParams _params;
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
        public GetTopTenAccounts(ILogger<GetTopTenAccounts> logger, IRepoFactory repoFactory, ICustomUtility utility)
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
        public virtual GetTopTenAccounts AssignParameters(GetTopTenAccountsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetTopTenAccountsDto>> Fetch()
        {
            _repoFactory.Init(_params);
            _flexAppContext = _params.GetAppContext();  //do not remove this line
            userId = _flexAppContext.UserId;

            var result = await Build<LoanAccount>().SelectTo<GetTopTenAccountsDto>().OrderBy(a => a.CollectorAllocationExpiryDate).ToListAsync();
            result.ForEach(x =>
            {
                x.ExpiresInDays = ((x.CollectorAllocationExpiryDate?.Date ?? DateTime.Now.Date) - DateTime.Now.Date).Days;
                x.AllocationExpiryColor = _utility.ReturnExpiryColorBasedOnExpiryDays(x.ExpiresInDays);
            }
            );

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

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>()
                                    .BySecondaryAllocation(userId)
                                    .OrderByDescending(a => a.BOM_POS)
                                    .OrderByDescending(a => a.PTPDate)
                                    .Take(10);

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetTopTenAccountsParams : DtoBridge
    {
    }
}