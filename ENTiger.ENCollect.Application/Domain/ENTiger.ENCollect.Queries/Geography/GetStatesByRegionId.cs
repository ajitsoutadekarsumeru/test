using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.GeographyModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetStatesByRegionId : FlexiQueryEnumerableBridgeAsync<State, GetStatesByRegionIdDto>
    {
        protected readonly ILogger<GetStatesByRegionId> _logger;
        protected GetStatesByRegionIdParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetStatesByRegionId(ILogger<GetStatesByRegionId> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetStatesByRegionId AssignParameters(GetStatesByRegionIdParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetStatesByRegionIdDto>> Fetch()
        {
            return await Build<State>().SelectTo<GetStatesByRegionIdDto>().ToListAsync();
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override IQueryable<T> Build<T>()
        {
            _repoFactory.Init(_params);

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().ByStateRegionmap(_params.regionId)
                                     .ByDeleteState(); ;

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetStatesByRegionIdParams : DtoBridge
    {
        public string regionId { get; set; }
    }
}