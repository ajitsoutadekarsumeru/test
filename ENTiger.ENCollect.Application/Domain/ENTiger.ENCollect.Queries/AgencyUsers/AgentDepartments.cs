using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public class AgentDepartments : FlexiQueryEnumerableBridgeAsync<Department, AgentDepartmentsDto>
    {
        protected readonly ILogger<AgentDepartments> _logger;
        protected AgentDepartmentsParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public AgentDepartments(ILogger<AgentDepartments> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual AgentDepartments AssignParameters(AgentDepartmentsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<AgentDepartmentsDto>> Fetch()
        {
            var result = await Build<Department>().SelectTo<AgentDepartmentsDto>().ToListAsync();

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

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().AgentDepartments();

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class AgentDepartmentsParams : DtoBridge
    {
    }
}