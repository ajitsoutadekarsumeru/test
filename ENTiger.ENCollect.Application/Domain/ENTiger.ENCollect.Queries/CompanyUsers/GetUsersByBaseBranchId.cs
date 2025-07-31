using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetUsersByBaseBranchId : FlexiQueryEnumerableBridgeAsync<CompanyUser, GetUsersByBaseBranchIdDto>
    {
        protected readonly ILogger<GetUsersByBaseBranchId> _logger;
        protected GetUsersByBaseBranchIdParams _params;
        protected readonly IRepoFactory _repoFactory;
        private CompanyUserWorkflowState state;

        //CompanyUserWorkflowState state = WorkflowStateFactory.GetCompanyUserWFState("approved");

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetUsersByBaseBranchId(ILogger<GetUsersByBaseBranchId> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetUsersByBaseBranchId AssignParameters(GetUsersByBaseBranchIdParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetUsersByBaseBranchIdDto>> Fetch()
        {
            var result = await Build<CompanyUser>().SelectTo<GetUsersByBaseBranchIdDto>().ToListAsync();

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

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().ByCompanyUserWFState(state).ByCompanyBranchId(_params.BaseBranchId);

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetUsersByBaseBranchIdParams : DtoBridge
    {
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Invalid BaseBranchId")]
        public string? BaseBranchId { get; set; }
    }
}