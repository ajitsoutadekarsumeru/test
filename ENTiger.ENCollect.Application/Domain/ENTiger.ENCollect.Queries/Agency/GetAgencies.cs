using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetAgencies : FlexiQueryEnumerableBridgeAsync<Agency, GetAgenciesDto>
    {
        protected readonly ILogger<GetAgencies> _logger;
        protected GetAgenciesParams _params;
        protected readonly IRepoFactory _repoFactory;
        private AgencyWorkflowState state;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetAgencies(ILogger<GetAgencies> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetAgencies AssignParameters(GetAgenciesParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetAgenciesDto>> Fetch()
        {
            state = WorkflowStateFactory.GetCollectionAgencyWFState("approved");
            //GetAgencies collection = new GetAgencies();
            //Collection<Agency> collection2;
            var result = await Build<Agency>().SelectTo<GetAgenciesDto>().ToListAsync();

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
                      .IncludeAgencyType()
                      .Where(x => (x as Agency).AgencyType != null
                       && (x as Agency).AgencyWorkflowState != null
                       && (x as Agency).AgencyType.SubType != AgencySubTypeEnum.TeleCalling.Value
                       && (x as Agency).AgencyWorkflowState.Name == state.Name);

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetAgenciesParams : DtoBridge
    {
    }
}