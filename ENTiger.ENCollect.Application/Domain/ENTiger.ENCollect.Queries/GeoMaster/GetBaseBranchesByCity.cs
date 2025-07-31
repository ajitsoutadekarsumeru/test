using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.GeoMasterModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetBaseBranchesByCity : FlexiQueryEnumerableBridgeAsync<GeoMaster, GetBaseBranchesByCityDto>
    {
        protected readonly ILogger<GetBaseBranchesByCity> _logger;
        protected GetBaseBranchesByCityParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetBaseBranchesByCity(ILogger<GetBaseBranchesByCity> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetBaseBranchesByCity AssignParameters(GetBaseBranchesByCityParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetBaseBranchesByCityDto>> Fetch()
        {
            var result = await Build<GeoMaster>().GroupBy(t => new { t.BaseBranchId, t.CITY })//.SelectTo<GetBaseBranchesByCityDto>().ToListAsync();
                                .Select(x => new GetBaseBranchesByCityDto()
                                {
                                    Id = x.FirstOrDefault().BaseBranchId,
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
                                    .IncludeBaseBranch();

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetBaseBranchesByCityParams : DtoBridge
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