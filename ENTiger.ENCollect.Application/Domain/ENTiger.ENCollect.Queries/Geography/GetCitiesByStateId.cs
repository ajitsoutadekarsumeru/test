using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.GeographyModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetCitiesByStateId : FlexiQueryEnumerableBridgeAsync<Cities, GetCitiesByStateIdDto>
    {
        protected readonly ILogger<GetCitiesByStateId> _logger;
        protected GetCitiesByStateIdParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetCitiesByStateId(ILogger<GetCitiesByStateId> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetCitiesByStateId AssignParameters(GetCitiesByStateIdParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetCitiesByStateIdDto>> Fetch()
        {
            var result = await Build<Cities>().SelectTo<GetCitiesByStateIdDto>().ToListAsync();

            return result.OrderByDescending(a => a.CreatedDate).ThenByDescending(b => b.LastModifiedDate).ToList();
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override IQueryable<T> Build<T>()
        {
            _repoFactory.Init(_params);

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().ByCityStateMap(_params.stateId)
                                    .ByDeleteCity(); ;

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetCitiesByStateIdParams : DtoBridge
    {
        public string stateId { get; set; }
    }
}