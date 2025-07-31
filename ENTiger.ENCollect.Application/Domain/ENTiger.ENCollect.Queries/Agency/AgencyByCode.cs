using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public class AgencyByCode : FlexiQueryBridgeAsync<Agency, AgencyByCodeDto>
    {
        protected readonly ILogger<AgencyByCode> _logger;
        protected AgencyByCodeParams _params;
        protected readonly IRepoFactory _repoFactory;
        protected AgencyWorkflowState agencyWorkflowState;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public AgencyByCode(ILogger<AgencyByCode> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual AgencyByCode AssignParameters(AgencyByCodeParams @params)
        {
            _params = @params;

            agencyWorkflowState = WorkflowStateFactory.GetCollectionAgencyWFState("approved");

            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<AgencyByCodeDto> Fetch()
        {
            var result = await Build<Agency>().SelectTo<AgencyByCodeDto>().FirstOrDefaultAsync();

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
                                    .ByAgencyWorkFlowState(agencyWorkflowState)
                                    .ByCode(_params.code);
            return query;
        }
    }

    public class AgencyByCodeParams : DtoBridge
    {
        public string code { get; set; }
    }
}