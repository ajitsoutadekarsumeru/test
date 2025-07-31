using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public class agencylist : FlexiQueryEnumerableBridgeAsync<Agency, agencylistDto>
    {
        protected readonly ILogger<agencylist> _logger;
        protected agencylistParams _params;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        string agencyId = string.Empty;
        ApplicationUser usertype = new ApplicationUser();

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public agencylist(ILogger<agencylist> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual agencylist AssignParameters(agencylistParams @params)
        {
            _params = @params;
            return this;
        }

        public override async Task<IEnumerable<agencylistDto>> Fetch()
        {

            _repoFactory.Init(_params);
            _flexAppContext = _params.GetAppContext();  //do not remove this line
            string userId = _flexAppContext.UserId;

            usertype = await _repoFactory.GetRepo().FindAll<ApplicationUser>().Where(a => a.Id == userId).FirstOrDefaultAsync();
            if (usertype?.GetType() == typeof(AgencyUser))
            {
                AgencyUser agencyUser = await _repoFactory.GetRepo().FindAll<AgencyUser>().IncludeAgencyUserAgency().Where(a => a.Id == userId).FirstOrDefaultAsync();
                agencyId = agencyUser?.Agency.Id;
            }

            var result = await Build<Agency>().SelectTo<agencylistDto>().ToListAsync();

            return result;
        }

        protected override IQueryable<T> Build<T>()
        {
            _repoFactory.Init(_params);

            if (usertype?.GetType() == typeof(AgencyUser))
            {
                IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>()
                                                .ByAgencyIdWithUserType(agencyId, usertype)
                                                .ByApprovedAgency();
                return query;
            }
            else
            {
                IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>()
                                            .ByApprovedAgency();
                return query;
            }
        }
    }

    public class agencylistParams : DtoBridge
    {
    }
}