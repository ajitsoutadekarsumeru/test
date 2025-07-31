using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetTodaysPTPAccounts : FlexiQueryEnumerableBridgeAsync<LoanAccount, GetTodaysPTPAccountsDto>
    {
        protected readonly ILogger<GetTodaysPTPAccounts> _logger;
        protected GetTodaysPTPAccountsParams _params;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        private string userId;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetTodaysPTPAccounts(ILogger<GetTodaysPTPAccounts> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetTodaysPTPAccounts AssignParameters(GetTodaysPTPAccountsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetTodaysPTPAccountsDto>> Fetch()
        {
            _flexAppContext = _params.GetAppContext();
            userId = _flexAppContext.UserId;
            var result = await Build<LoanAccount>().SelectTo<GetTodaysPTPAccountsDto>().ToListAsync();

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
                                    .ByTodaysPTP()
                                    .ByCodePTP()
                                    .ByCollectorId(userId);

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetTodaysPTPAccountsParams : DtoBridge
    {
        //  public string? LoggedInParty { get; set; }
    }
}