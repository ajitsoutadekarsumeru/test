using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetUsersByName : FlexiQueryEnumerableBridgeAsync<ApplicationUser, GetUsersByNameDto>
    {
        protected readonly ILogger<GetUsersByName> _logger;
        protected GetUsersByNameParams _params;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IRepoFactory _repoFactory;
        private string userId, tenantId;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetUsersByName(ILogger<GetUsersByName> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetUsersByName AssignParameters(GetUsersByNameParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetUsersByNameDto>> Fetch()
        {
            _repoFactory.Init(_params);
            _flexAppContext = _params.GetAppContext();
            userId = _flexAppContext.UserId;
            tenantId = _flexAppContext.TenantId;

            IEnumerable<GetUsersByNameDto> result = Enumerable.Empty<GetUsersByNameDto>();

            var user = await _repoFactory.GetRepo().FindAll<ApplicationUser>().Where(x => x.Id == userId).FirstOrDefaultAsync();
            if (user != null)
            {
                if (user is AgencyUser)
                {
                    return await FetchAgencyUsers();
                }
                else
                {
                    return await FetchCompanyUsers();
                }
            }
            return Enumerable.Empty<GetUsersByNameDto>();
        }

        private async Task<IEnumerable<GetUsersByNameDto>> FetchAgencyUsers()
        {
            var loggedInAgencyId = await _repoFactory.GetRepo().FindAll<AgencyUser>()
                                       .Where(c => c.Id == userId)
                                       .Select(a => a.AgencyId)
                                       .FirstOrDefaultAsync();

            var users = await _repoFactory.GetRepo().FindAll<AgencyUser>()
                        .LoggedInuserAgencyId(loggedInAgencyId)
                        .ByAgencyUserWorkflowState("AgencyUserApproved")
                        .ByAgentName(_params.Name)
                        .Select(a => new GetUsersByNameDto
                        {
                            Id = a.Id,
                            FirstName = a.FirstName,
                            LastName = a.LastName,
                            AgentCode = a.CustomId
                        }).ToListAsync();

            return users;
        }

        private async Task<IEnumerable<GetUsersByNameDto>> FetchCompanyUsers()
        {
            var loggedInUserBaseBranchId = await _repoFactory.GetRepo().FindAll<CompanyUser>()
                                            .ByCompanyUserId(userId)
                                            .Select(a => a.BaseBranchId)
                                            .FirstOrDefaultAsync();

            var users =await _repoFactory.GetRepo().FindAll<CompanyUser>()
                        .ByCompanyBranchId(loggedInUserBaseBranchId)
                        .ByCompanyUserWorkFlowState("CompanyUserApproved")
                        .ByName(_params.Name)
                        .Select(a => new GetUsersByNameDto
                        {
                            Id = a.Id,
                            FirstName = a.FirstName,
                            LastName = a.LastName,
                            AgentCode = a.CustomId
                        }).ToListAsync();

            return users;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override IQueryable<T> Build<T>()
        {
            _repoFactory.Init(_params);

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>();

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetUsersByNameParams : DtoBridge
    {
        public string? Name { get; set; }
    }
}