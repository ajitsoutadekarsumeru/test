using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.GeographyModule
{
    /// <summary>
    ///
    /// </summary>
    public class SearchAreas : FlexiQueryEnumerableBridgeAsync<Area, SearchAreaDto>
    {
        protected readonly ILogger<SearchAreas> _logger;
        protected SearchAreasParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public SearchAreas(ILogger<SearchAreas> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual SearchAreas AssignParameters(SearchAreasParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<SearchAreaDto>> Fetch()
        {
            var result = await Build<Area>().SelectTo<SearchAreaDto>().ToListAsync();

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
                                    .ByAreaNickNameOrNameSearch(_params.query)
                                    .ByDeleteArea();

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class SearchAreasParams : DtoBridge
    {
        public string query { get; set; }
    }
}