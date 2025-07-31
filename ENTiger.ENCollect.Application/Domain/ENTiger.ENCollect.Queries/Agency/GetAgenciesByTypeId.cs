using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetAgenciesByTypeId : FlexiQueryEnumerableBridgeAsync<Agency, GetAgenciesByTypeIdDto>
    {
        protected readonly ILogger<GetAgenciesByTypeId> _logger;
        protected GetAgenciesByTypeIdParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetAgenciesByTypeId(ILogger<GetAgenciesByTypeId> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetAgenciesByTypeId AssignParameters(GetAgenciesByTypeIdParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetAgenciesByTypeIdDto>> Fetch()
        {
            var result = await Build<Agency>().SelectTo<GetAgenciesByTypeIdDto>().ToListAsync();

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

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().ByAgencyType(_params.AgencyTypeId);

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetAgenciesByTypeIdParams : DtoBridge
    {
        public string? AgencyTypeId { get; set; }
    }
}