using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.GeoMasterModule
{
    /// <summary>
    ///
    /// </summary>
    public class SearchBaseBranches : FlexiQueryEnumerableBridgeAsync<GeoMaster, SearchBaseBranchesDto>
    {
        protected readonly ILogger<SearchBaseBranches> _logger;
        protected SearchBaseBranchesParams _params;
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
        public virtual SearchBaseBranches AssignParameters(SearchBaseBranchesParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<SearchBaseBranchesDto>> Fetch()
        {
            var result = await Build<GeoMaster>().GroupBy(t => new { t.BaseBranchId, t.Area })//.SelectTo<SearchBaseBranchesDto>().ToListAsync();
                            .Select(x => new SearchBaseBranchesDto()
                            {
                                Id = x.Key.BaseBranchId,
                                Name = x.FirstOrDefault().BaseBranch.FirstName,
                                Code = x.FirstOrDefault().BaseBranch.CustomId
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

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>()
                                    .ByCountry(_params.Country)
                                    .ByRegion(_params.Region)
                                    .ByState(_params.State)
                                    .ByCity(_params.City)
                                    .ByArea(_params.Area)
                                    .IncludeBaseBranch();

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class SearchBaseBranchesParams : DtoBridge
    {
        [RegularExpression("^[a-zA-Z0-9- ]*$", ErrorMessage = "Invalid Country")]
        public string Country { get; set; }

        [RegularExpression("^[a-zA-Z0-9- ]*$", ErrorMessage = "Invalid Region")]
        public string Region { get; set; }

        [RegularExpression("^[a-zA-Z0-9- ]*$", ErrorMessage = "Invalid State")]
        public string State { get; set; }

        [RegularExpression("^[a-zA-Z0-9- ]*$", ErrorMessage = "Invalid City")]
        public string City { get; set; }

        [RegularExpression("^[a-zA-Z0-9- ]*$", ErrorMessage = "Invalid Area")]
        public string? Area { get; set; }
    }
}