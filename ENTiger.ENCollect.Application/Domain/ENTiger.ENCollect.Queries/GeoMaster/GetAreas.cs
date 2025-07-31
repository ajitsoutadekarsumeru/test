using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.GeoMasterModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetAreas : FlexiQueryEnumerableBridgeAsync<GeoMaster, GetAreasDto>
    {
        protected readonly ILogger<GetAreas> _logger;
        protected GetAreasParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetAreas(ILogger<GetAreas> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetAreas AssignParameters(GetAreasParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetAreasDto>> Fetch()
        {
            var result = await Build<GeoMaster>().GroupBy(t => new { t.Area })//.SelectTo<GetAreasDto>().ToListAsync();
                            .Select(x => new GetAreasDto()
                            {
                                Name = x.FirstOrDefault().Area,
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
    public class GetAreasParams : DtoBridge
    {
    }
}