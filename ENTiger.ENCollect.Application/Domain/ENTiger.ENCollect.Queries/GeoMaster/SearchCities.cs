using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.GeoMasterModule
{
    /// <summary>
    ///
    /// </summary>
    public class SearchCities : FlexiQueryEnumerableBridgeAsync<GeoMaster, SearchCitiesDto>
    {
        protected readonly ILogger<SearchCities> _logger;
        protected SearchCitiesParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public SearchCities(ILogger<SearchCities> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual SearchCities AssignParameters(SearchCitiesParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<SearchCitiesDto>> Fetch()
        {
            var result = await Build<GeoMaster>().GroupBy(t => new { t.CITY })//.SelectTo<SearchCitiesDto>().ToListAsync();
                                .Select(x => new SearchCitiesDto()
                                {
                                    Name = x.FirstOrDefault().CITY,
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

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().ByCountry(_params.Country)
                                    .ByRegion(_params.Region).ByState(_params.State);

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class SearchCitiesParams : DtoBridge
    {
        [RegularExpression("^[a-zA-Z0-9- ]*$", ErrorMessage = "Invalid Country")]
        public string Country { get; set; }

        [RegularExpression("^[a-zA-Z0-9- ]*$", ErrorMessage = "Invalid Region")]
        public string Region { get; set; }

        [RegularExpression("^[a-zA-Z0-9- ]*$", ErrorMessage = "Invalid State")]
        public string State { get; set; }
    }
}