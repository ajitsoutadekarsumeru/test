using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CategoryModule
{
    /// <summary>
    ///
    /// </summary>
    public class SearchCategoryItems : FlexiQueryEnumerableBridgeAsync<CategoryItem, SearchCategoryItemsDto>
    {
        protected readonly ILogger<SearchCategoryItems> _logger;
        protected SearchCategoryItemsParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public SearchCategoryItems(ILogger<SearchCategoryItems> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual SearchCategoryItems AssignParameters(SearchCategoryItemsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<SearchCategoryItemsDto>> Fetch()
        {
            return await Build<CategoryItem>().SelectTo<SearchCategoryItemsDto>().ToListAsync();
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override IQueryable<T> Build<T>()
        {
            _repoFactory.Init(_params);

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().Where(a => (a.Code == _params.SearchParam || a.Name.StartsWith(_params.SearchParam))
                                            && a.CategoryMasterId == _params.MasterId);
            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class SearchCategoryItemsParams : DtoBridge
    {
        public string? SearchParam { get; set; }
        public string? MasterId { get; set; }
    }
}