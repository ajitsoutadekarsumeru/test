using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.BaseBranchesModule
{
    /// <summary>
    ///
    /// </summary>
    public class BaseBranchByUserId : FlexiQueryEnumerableBridgeAsync<BaseBranch, BaseBranchByUserIdDto>
    {
        protected readonly ILogger<BaseBranchByUserId> _logger;
        protected BaseBranchByUserIdParams _params;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        private List<string> _accountabilityTypes;
        private string? _userbranchId;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public BaseBranchByUserId(ILogger<BaseBranchByUserId> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual BaseBranchByUserId AssignParameters(BaseBranchByUserIdParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<BaseBranchByUserIdDto>> Fetch()
        {
            _flexAppContext = _params.GetAppContext();  //do not remove this line

            string userId = _flexAppContext.UserId;

            _repoFactory.Init(_params);

            List<Accountability> accountability = await _repoFactory.GetRepo().FindAll<Accountability>().Where(a => a.ResponsibleId == userId).ToListAsync();
            if (accountability != null)
            {
                _accountabilityTypes = accountability.Select(x => x.AccountabilityTypeId).Distinct().ToList();
            }
            _userbranchId = await _repoFactory.GetRepo().FindAll<CompanyUser>().ByCompanyUserId(userId)
                                    .Select(x => x.BaseBranchId).FirstOrDefaultAsync();

            var result = await Build<BaseBranch>().SelectTo<BaseBranchByUserIdDto>().ToListAsync();

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override IQueryable<T> Build<T>()
        {
            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>();

            //Build Your Query Here
            if (!_accountabilityTypes.Contains("bihp"))
            {
                query = query.Where(x => x.Id == _userbranchId);
            }

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class BaseBranchByUserIdParams : DtoBridge
    {
    }
}