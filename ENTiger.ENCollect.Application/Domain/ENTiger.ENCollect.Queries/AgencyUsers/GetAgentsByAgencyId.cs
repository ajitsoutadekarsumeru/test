using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetAgentsByAgencyId : FlexiQueryEnumerableBridgeAsync<AgencyUser, GetAgentsByAgencyIdDto>
    {
        protected readonly ILogger<GetAgentsByAgencyId> _logger;
        protected GetAgentsByAgencyIdParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetAgentsByAgencyId(ILogger<GetAgentsByAgencyId> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetAgentsByAgencyId AssignParameters(GetAgentsByAgencyIdParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetAgentsByAgencyIdDto>> Fetch()
        {
            var result = await Build<AgencyUser>().SelectTo<GetAgentsByAgencyIdDto>().ToListAsync();

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

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().Where(a => a.AgencyId == _params.Id);

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetAgentsByAgencyIdParams : DtoBridge
    {
        public string? Id { get; set; }
    }
}