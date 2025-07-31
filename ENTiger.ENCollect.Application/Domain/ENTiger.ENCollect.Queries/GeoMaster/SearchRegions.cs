using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.GeoMasterModule
{
    /// <summary>
    ///
    /// </summary>
    public class SearchRegions : FlexiQueryEnumerableBridgeAsync<GeoMaster, SearchRegionsDto>
    {
        protected readonly ILogger<SearchRegions> _logger;
        protected SearchRegionsParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public SearchRegions(ILogger<SearchRegions> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual SearchRegions AssignParameters(SearchRegionsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<SearchRegionsDto>> Fetch()
        {
            var result = await Build<GeoMaster>().GroupBy(t => new { t.Region })//.SelectTo<SearchRegionsDto>().ToListAsync();
                                .Select(x => new SearchRegionsDto()
                                {
                                    Name = x.FirstOrDefault().Region,
                                }).ToListAsync();

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

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().ByCountry(_params.Country);

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class SearchRegionsParams : DtoBridge
    {
        [RegularExpression("^[a-zA-Z0-9- ]*$", ErrorMessage = "Invalid Country")]
        [Required]
        public string Country { get; set; }
    }
}