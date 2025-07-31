using ENTiger.ENCollect.BaseBranchesModule;
using Microsoft.EntityFrameworkCore;
using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class GetUsersByIds : FlexiQueryEnumerableBridgeAsync<ApplicationUser,GetUsersByIdsDto>
    {
        protected readonly ILogger<GetUsersByIds> _logger;
        protected GetUsersByIdsParams _params;
        protected readonly RepoFactory _repoFactory;
        
        protected FlexAppContextBridge? _flexAppContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetUsersByIds(ILogger<GetUsersByIds> logger, RepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetUsersByIds AssignParameters(GetUsersByIdsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetUsersByIdsDto>> Fetch()
        {
            _flexAppContext = _params.GetAppContext();
            var result = await Build<ApplicationUser>().SelectTo<GetUsersByIdsDto>().ToListAsync();
            foreach(var user in result)
            {
                var appUser = await _repoFactory.GetRepo().FindAll<ApplicationUser>()
                                   .Where(x => x.Id == user.Id)
                                   .FirstOrDefaultAsync();

                if (appUser is CompanyUser)
                {
                    var baseBranch = await _repoFactory.GetRepo().FindAll<CompanyUser>()
                                            .Where(x => x.Id == appUser.Id)
                                            .Select(s => s.BaseBranch.FirstName)
                                            .FirstOrDefaultAsync();

                    user.BaseBranch = baseBranch;
                }
            }
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
                                        .Where(w => _params.UserIds.Contains(w.Id));

            return query;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class GetUsersByIdsParams : DtoBridge
    {
        public List<string> UserIds { get; set; }
    }
}
