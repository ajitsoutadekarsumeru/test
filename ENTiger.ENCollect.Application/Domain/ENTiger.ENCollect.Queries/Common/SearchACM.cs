using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public class SearchACM : FlexiQueryEnumerableBridgeAsync<UserAccessRights, SearchACMDto>
    {
        protected readonly ILogger<SearchACM> _logger;
        protected SearchACMParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public SearchACM(ILogger<SearchACM> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual SearchACM AssignParameters(SearchACMParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<SearchACMDto>> Fetch()
        {
            var accessRights = await Build<UserAccessRights>().ToListAsync();

            var result = accessRights.SelectMany(obj => obj.MenuMaster.SubMenus
                                .SelectMany(sub => sub.Actions
                                .Select(action => new SearchACMDto
                                {
                                    Menu = obj.MenuMaster?.MenuName,
                                    SubMenu = sub.SubMenuName,
                                    Id = action.Id,
                                    Scope = action.Name,
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
            _repoFactory.Init(_params);

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>()
                                    .Where(x => !x.IsDeleted && x.IsFrontEnd && x.IsMobile == _params.IsMobile && x.AccountabilityTypeId == _params.Accountability)
                                    .FlexInclude(x => x.MenuMaster).FlexInclude("MenuMaster.SubMenus").FlexInclude("MenuMaster.SubMenus.Actions");
            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class SearchACMParams : DtoBridge
    {
        [Required]
        public string? Accountability { get; set; }

        public bool IsMobile { get; set; }
    }
}