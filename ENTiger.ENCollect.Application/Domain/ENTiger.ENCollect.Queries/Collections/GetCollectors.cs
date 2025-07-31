using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetCollectors : FlexiQueryEnumerableBridgeAsync<ApplicationUser, GetCollectorsDto>
    {
        protected readonly ILogger<GetCollectors> _logger;
        protected GetCollectorsParams _params;
        protected readonly IRepoFactory _repoFactory;
        private AgencyUserWorkflowState state;
        protected FlexAppContextBridge? _flexAppContext;
        private string CollectorId, BaseBranchId;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetCollectors(ILogger<GetCollectors> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetCollectors AssignParameters(GetCollectorsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetCollectorsDto>> Fetch()
        {
            _flexAppContext = _params.GetAppContext();
            CollectorId = _flexAppContext.UserId;

            //For testing
            // tenantId = _flexAppContext.TenantId;
            // CollectorId = "39f48fedf48019cfe762005b6b550085";/*for testing of CompanyUser*/
            // CollectorId = "39f494b726312680271bb35c8c0c657d";/*for testing of AgencyUser*/
            // BaseBranchId = "998CB8820CE042899295427B7BCEA200";

            state = WorkflowStateFactory.GetCollectionAgencyUserWFState("Approved");

            IEnumerable<GetCollectorsDto> result = Enumerable.Empty<GetCollectorsDto>();

            if (!string.IsNullOrEmpty(CollectorId))
            {
                var loggedInUserParty = await Build<ApplicationUser>().Where(x => x.Id == CollectorId).FirstOrDefaultAsync();

                if (loggedInUserParty != null)
                {
                    if (loggedInUserParty.GetType() == typeof(AgencyUser))
                    {
                        AgencyUser loggedInAgencyUser =await Build<AgencyUser>().Where(x => x.Id == CollectorId).FirstOrDefaultAsync();
                        result = await Build<AgencyUser>().Where(x => x.AgencyId == loggedInAgencyUser.AgencyId).SelectTo<GetCollectorsDto>().ToListAsync();
                    }
                    else if (loggedInUserParty.GetType() == typeof(CompanyUser))
                    {
                        CompanyUser loggedInCompanyUser = await Build<CompanyUser>().Where(x => x.Id == CollectorId).FirstOrDefaultAsync();

                        result = await Build<CompanyUser>().Where(x => x.BaseBranchId == loggedInCompanyUser.BaseBranchId).SelectTo<GetCollectorsDto>().ToListAsync();
                    }
                }
            }

            // Return the result list
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

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>();

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetCollectorsParams : DtoBridge
    {
    }
}