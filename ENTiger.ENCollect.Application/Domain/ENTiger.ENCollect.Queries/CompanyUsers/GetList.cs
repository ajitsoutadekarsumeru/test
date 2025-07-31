using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetList : FlexiQueryEnumerableBridgeAsync<CompanyUser, GetListsDto>
    {
        protected readonly ILogger<GetList> _logger;
        protected GetListParams _params;
        protected readonly IRepoFactory _repoFactory;

        //CompanyUserWorkflowState state = new CompanyUserWorkflowState();
        private CompanyUserWorkflowState state = WorkflowStateFactory.GetCompanyUserWFState("approved");

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetList(ILogger<GetList> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetList AssignParameters(GetListParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetListsDto>> Fetch()
        {
            var result = await Build<CompanyUser>().SelectTo<GetListsDto>().ToListAsync();

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

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().ByCompanyUserWFState(state);

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetListParams : DtoBridge
    {
        public bool IsFrontEnd { get; set; }
    }
}