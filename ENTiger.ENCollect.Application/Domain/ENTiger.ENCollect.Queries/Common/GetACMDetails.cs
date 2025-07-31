using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetACMDetails : FlexiQueryEnumerableBridgeAsync<UserAccessRights, GetACMDetailsDto>
    {
        protected readonly ILogger<GetACMDetails> _logger;
        protected GetACMDetailsParams _params;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        private string accountabilityTypeId = string.Empty;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetACMDetails(ILogger<GetACMDetails> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetACMDetails AssignParameters(GetACMDetailsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetACMDetailsDto>> Fetch()
        {
            _repoFactory.Init(_params);
            _flexAppContext = _params.GetAppContext();  //do not remove this line
            string userId = _flexAppContext.UserId;

            accountabilityTypeId = await _repoFactory.GetRepo().FindAll<Accountability>()
                                    .Where(x => x.ResponsibleId == userId && !x.IsDeleted)
                                    .Select(x => x.AccountabilityTypeId).FirstOrDefaultAsync();

            var accessRights = await Build<UserAccessRights>().ToListAsync();

            var result = accessRights.SelectMany(obj => obj.MenuMaster.SubMenus
                                .SelectMany(sub => sub.Actions
                                .Select(action => new GetACMDetailsDto
                                {
                                    MenuMasterName = obj.MenuMaster?.MenuName,
                                    SubMenuMasterName = sub.SubMenuName,
                                    ActionMasterName = action.Name,
                                    HasAccess = action.HasAccess
                                })))
                                .ToList();

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override IQueryable<T> Build<T>()
        {
            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>()
                                    .Where(x => !x.IsDeleted && x.IsFrontEnd && x.IsMobile == _params.IsMobile
                                    && x.AccountabilityTypeId == accountabilityTypeId)
                                   .FlexInclude(x => x.MenuMaster).FlexInclude("MenuMaster.SubMenus")
                                   .FlexInclude("MenuMaster.SubMenus.Actions");

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetACMDetailsParams : DtoBridge
    {
        public bool IsMobile { get; set; }
    }
}