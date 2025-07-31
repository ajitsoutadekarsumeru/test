using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.GeoMasterModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetRegions : FlexiQueryEnumerableBridgeAsync<GeoMaster, GetRegionsDto>
    {
        protected readonly ILogger<GetRegions> _logger;
        protected GetRegionsParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetRegions(ILogger<GetRegions> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetRegions AssignParameters(GetRegionsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetRegionsDto>> Fetch()
        {
            var result = await Build<GeoMaster>().GroupBy(t => new { t.Region })//.SelectTo<GetRegionsDto>().ToListAsync();
                            .Select(x => new GetRegionsDto()
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

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>();

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetRegionsParams : DtoBridge
    {
    }
}