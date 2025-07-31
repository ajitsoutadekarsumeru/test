using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public class CompanyUserDetails : FlexiQueryBridgeAsync<CompanyUser, CompanyUserDetailsDto>
    {
        protected readonly ILogger<CompanyUserDetails> _logger;
        protected CompanyUserDetailsParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public CompanyUserDetails(ILogger<CompanyUserDetails> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual CompanyUserDetails AssignParameters(CompanyUserDetailsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<CompanyUserDetailsDto> Fetch()
        {
            var result = await Build<CompanyUser>().SelectTo<CompanyUserDetailsDto>().FirstOrDefaultAsync();

            var workFlowStates = await _repoFactory.GetRepo().FindAllObjectWithState<CompanyUserWorkflowState>()
                                    .Where(a => a.TFlexId == result.Id)
                                    .OrderByDescending(a => a.StateChangedDate)
                                    .SelectTo<ChangeLogInfoDto>().ToListAsync();

            var userIds = workFlowStates.Select(x => x.ChangedByUserId).Distinct().ToArray();

            var users = await _repoFactory.GetRepo().FindAll<ApplicationUser>().Where(x => userIds.Contains(x.Id)).ToListAsync();
            foreach (var state in workFlowStates)
            {
                var stateChangedUser = users.Where(x => x.Id == state.ChangedByUserId).FirstOrDefault();
                state.ChangedByUserName = stateChangedUser != null ? stateChangedUser.FirstName + " " + stateChangedUser.LastName : string.Empty;
            }

            result.ChangeLogInfo = workFlowStates;

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

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().Where(t => t.Id == _params.Id)
                                    .IncludeUserProductScope()
                                    .IncludeUserGeoScope()
                                    .IncludeUserBucketScope()
                                    //.IncludeReportingAgencies()
                                    //.IncludeSupervisingManager()
                                    //.IncludeScopeOfWorkSupervisingManager()
                                    .IncludeDesignation()
                                    .CompanyUserIncludePlaceOfWork()
                                    .IncludeCompanyUserWorkflow()
                                    .IncludeWallet();
            return query;
        }
    }

    public class CompanyUserDetailsParams : DtoBridge
    {
        public string Id { get; set; }
    }
}