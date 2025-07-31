using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAccountsByIds : FlexiQueryEnumerableBridgeAsync<LoanAccount, GetAccountsByIdsDto>
    {
        protected readonly ILogger<GetAccountsByIds> _logger;
        protected GetAccountsByIdsParams _params;
        protected readonly RepoFactory _repoFactory;

        protected FlexAppContextBridge? _flexAppContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetAccountsByIds(ILogger<GetAccountsByIds> logger, RepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetAccountsByIds AssignParameters(GetAccountsByIdsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetAccountsByIdsDto>> Fetch()
        {
            _flexAppContext = _params.GetAppContext();

            var result = await Build<LoanAccount>().SelectTo<GetAccountsByIdsDto>().ToListAsync();

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
                                        .ByAccountIds(_params.AccountIds);

            return query;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class GetAccountsByIdsParams : DtoBridge
    {
        public List<string> AccountIds { get; set; }
    }
}
