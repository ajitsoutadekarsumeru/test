using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public class ParentAgencyByName : FlexiQueryEnumerableBridgeAsync<Agency, ParentAgencyByNameDto>
    {
        protected readonly ILogger<ParentAgencyByName> _logger;
        protected ParentAgencyByNameParams _params;
        protected readonly IRepoFactory _repoFactory;
        private AgencyWorkflowState workflowState;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public ParentAgencyByName(ILogger<ParentAgencyByName> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual ParentAgencyByName AssignParameters(ParentAgencyByNameParams @params)
        {
            _params = @params;
            workflowState = WorkflowStateFactory.GetCollectionAgencyWFState("approved");
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<ParentAgencyByNameDto>> Fetch()
        {
            var result = await Build<Agency>().SelectTo<ParentAgencyByNameDto>().ToListAsync();

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
                .ByParent()
                .ByAgencyWorkFlowState(workflowState)
                .ByParentAgencyName(_params.name);
            return query;
        }
    }

    public class ParentAgencyByNameParams : DtoBridge
    {
        public string name { get; set; }
    }
}