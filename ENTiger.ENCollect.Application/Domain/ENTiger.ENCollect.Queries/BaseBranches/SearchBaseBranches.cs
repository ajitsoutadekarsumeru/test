using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.BaseBranchesModule
{
    /// <summary>
    ///
    /// </summary>
    public class SearchBaseBranches : FlexiQueryEnumerableBridgeAsync<BaseBranch, SearchBaseBranchDto>
    {
        protected readonly ILogger<SearchBaseBranches> _logger;
        protected SearchBaseBranchParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public SearchBaseBranches(ILogger<SearchBaseBranches> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual SearchBaseBranches AssignParameters(SearchBaseBranchParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<SearchBaseBranchDto>> Fetch()
        {
            var result = await Build<BaseBranch>().SelectTo<SearchBaseBranchDto>().ToListAsync();

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

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().ByPartialBaseBranchName(_params.Query);

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class SearchBaseBranchParams : DtoBridge
    {
        public string Query { get; set; }
    }
}