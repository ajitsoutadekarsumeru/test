using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetCollectionById : FlexiQueryBridgeAsync<Collection, GetCollectionByIdDto>
    {
        protected readonly ILogger<GetCollectionById> _logger;
        protected GetCollectionByIdParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetCollectionById(ILogger<GetCollectionById> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual GetCollectionById AssignParameters(GetCollectionByIdParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<GetCollectionByIdDto> Fetch()
        {
            var result = await Build<Collection>().SelectTo<GetCollectionByIdDto>().FirstOrDefaultAsync();

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

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().ByCollectionId(_params.Id);
            return query;
        }
    }

    public class GetCollectionByIdParams : DtoBridge
    {
        public string? Id { get; set; }
    }
}