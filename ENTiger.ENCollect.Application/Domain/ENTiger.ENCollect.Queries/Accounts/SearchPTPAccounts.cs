using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public class SearchPTPAccounts : FlexiQueryEnumerableBridgeAsync<LoanAccount, SearchPTPAccountsDto>
    {
        protected readonly ILogger<SearchPTPAccounts> _logger;
        protected SearchPTPAccountsParams _params;
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
        public SearchPTPAccounts(ILogger<SearchPTPAccounts> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual SearchPTPAccounts AssignParameters(SearchPTPAccountsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<SearchPTPAccountsDto>> Fetch()
        {
            _flexAppContext = _params.GetAppContext();/*Cmd.Dto.GetAppContext();*/
            userId = _flexAppContext.UserId;
            var result = await Build<LoanAccount>().SelectTo<SearchPTPAccountsDto>().ToListAsync();

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
                                    .ByPTP(_params.FromDate, _params.ToDate)
                                    .ByCodePTP()
                                    .ByCollectorId(userId);

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class SearchPTPAccountsParams : DtoBridge
    {
        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }
    }
}