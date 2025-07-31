using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.CollectionBatchesModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetBatchCollections : FlexiQueryPagedListBridgeAsync<Collection, GetBatchCollectionsParams, GetBatchCollectionsDto, FlexAppContextBridge>
    {
        protected readonly ILogger<GetBatchCollections> _logger;
        protected GetBatchCollectionsParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetBatchCollections(ILogger<GetBatchCollections> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetBatchCollections AssignParameters(GetBatchCollectionsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<FlexiPagedList<GetBatchCollectionsDto>> Fetch()
        {
            var projection = await Build<Collection>().SelectTo<GetBatchCollectionsDto>().ToListAsync();

            var result = BuildPagedOutput(projection);

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
                                    .ByCollectionBatchId(_params.Id);

            _params.take = _params.take == 0 ? 50 : _params.take;
            //Build Your Query With All Parameters Here

            query = CreatePagedQuery<T>(query, _params.PageNumber, _params.skip, _params.take);

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetBatchCollectionsParams : PagedQueryParamsDtoBridge
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public int take { get; set; }

        [Required]
        public int skip { get; set; }
    }
}