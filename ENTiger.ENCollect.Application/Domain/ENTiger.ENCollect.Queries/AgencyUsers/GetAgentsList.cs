using Elastic.Clients.Elasticsearch.Core.Search;
using Elastic.Clients.Elasticsearch;
using ENTiger.ENCollect.CollectionsModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using DocumentFormat.OpenXml.Spreadsheet;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetAgentsList : FlexiQueryEnumerableBridgeAsync<AgencyUser, GetAgentsListDto>
    {
        protected readonly ILogger<GetAgentsList> _logger;
        protected GetAgentsListParams _params;
        protected readonly IRepoFactory _repoFactory;
        private AgencyUserWorkflowState state;
        protected string _userId;
        private string agencyId;
        private ApplicationUser user;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetAgentsList(ILogger<GetAgentsList> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetAgentsList AssignParameters(GetAgentsListParams @params)
        {
            _params = @params;
            state = WorkflowStateFactory.GetCollectionAgencyUserWFState("approved");
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetAgentsListDto>> Fetch()
        {
            _repoFactory.Init(_params);

            _userId = _params.GetAppContext()?.UserId;

            user = await _repoFactory.GetRepo().FindAll<ApplicationUser>().Where(a => a.Id == _userId).FirstOrDefaultAsync();
            if (user?.GetType() == typeof(AgencyUser))
            {
                AgencyUser agencyUser = await _repoFactory.GetRepo().FindAll<AgencyUser>().IncludeAgencyUserAgency().Where(a => a.Id == _userId).FirstOrDefaultAsync();
                agencyId = agencyUser?.AgencyId;
            }

            var result = await Build<AgencyUser>().SelectTo<GetAgentsListDto>().ToListAsync();

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override IQueryable<T> Build<T>()
        {

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().ByWorkflowState(state);

            if (user?.GetType() == typeof(AgencyUser))
            {
                query = query.LoggedInuserAgencyId(agencyId);
            }

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetAgentsListParams : DtoBridge
    {
    }
}