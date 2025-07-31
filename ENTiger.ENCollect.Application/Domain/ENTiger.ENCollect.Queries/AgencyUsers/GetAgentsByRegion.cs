using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetAgentsByRegion : FlexiQueryEnumerableBridgeAsync<AgencyUser, GetAgentsByRegionDto>
    {
        protected readonly ILogger<GetAgentsByRegion> _logger;
        protected GetAgentsByRegionParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetAgentsByRegion(ILogger<GetAgentsByRegion> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetAgentsByRegion AssignParameters(GetAgentsByRegionParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetAgentsByRegionDto>> Fetch()
        {
            var result = await Build<AgencyUser>().SelectTo<GetAgentsByRegionDto>().ToListAsync();

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
                                    .IncludeUserGeoScope().ByAgencyUserByRegion(_params.Region);

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetAgentsByRegionParams : DtoBridge
    {
        public string? Region { get; set; }
    }
}