using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sumeru.Flex;

namespace ENTiger.ENCollect.UserSearchCriteriaModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetList : FlexiQueryEnumerableBridgeAsync<UserSearchCriteria, USGetListDto>
    {
        protected readonly ILogger<GetList> _logger;
        protected GetListParams _params;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        private ApplicationUser applicationUser;

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
        public override async Task<IEnumerable<USGetListDto>> Fetch()
        {
            var result = await Build<UserSearchCriteria>().Select(a => new USGetListDto
            {
                Id = a.Id,
                FilterName = a.filterName,
                UseCaseName = a.UseCaseName,
                FilterValues = JsonConvert.DeserializeObject(a.FilterValues)
            }).ToListAsync();

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
            _flexAppContext = _params.GetAppContext();  //do not remove this line
            string userId = _flexAppContext.UserId;

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().Where(a => a.UserId == userId).OrderBy(x => x.CreatedDate);

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetListParams : DtoBridge
    {
    }
}