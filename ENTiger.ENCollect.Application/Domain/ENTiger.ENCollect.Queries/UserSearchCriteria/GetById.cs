using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ENTiger.ENCollect.UserSearchCriteriaModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetById : FlexiQueryBridgeAsync<UserSearchCriteria, GetByIdDto>
    {
        protected readonly ILogger<GetById> _logger;
        protected GetByIdParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetById(ILogger<GetById> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual GetById AssignParameters(GetByIdParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<GetByIdDto> Fetch()
        {
            // var result = Build<UserSearchCriteria>().SelectTo<GetByIdDto>().FirstOrDefaultAsync();

            var result = await Build<UserSearchCriteria>().Select(a => new GetByIdDto
            {
                Id = a.Id,
                FilterName = a.filterName,
                UseCaseName = a.UseCaseName,
                FilterValues = JsonConvert.DeserializeObject<dynamic>(a.FilterValues)
            }).FirstOrDefaultAsync();
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

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().Where(t => t.Id == _params.Id);
            return query;
        }
    }

    public class GetByIdParams : DtoBridge
    {
        public string Id { get; set; }
    }
}