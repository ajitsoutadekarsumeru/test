using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetSecondaryAllocationDownload : FlexiQueryPagedListBridgeAsync<AllocationDownload, GetSecondaryAllocationDownloadParams, GetSecondaryAllocationDownloadDto, FlexAppContextBridge>
    {
        protected readonly ILogger<GetSecondaryAllocationDownload> _logger;
        protected GetSecondaryAllocationDownloadParams _params;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        private string? loggedInUserId;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetSecondaryAllocationDownload(ILogger<GetSecondaryAllocationDownload> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetSecondaryAllocationDownload AssignParameters(GetSecondaryAllocationDownloadParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<FlexiPagedList<GetSecondaryAllocationDownloadDto>> Fetch()
        {
            _flexAppContext = _params.GetAppContext();
            loggedInUserId = _flexAppContext.UserId;
            var projection = await Build<AllocationDownload>().SelectTo<GetSecondaryAllocationDownloadDto>().ToListAsync();

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
                           .ByAllocationBatchUserName(loggedInUserId)
                           .ByBatchAllocationType(_params.AllocationType)
                           .OrderByDescending(x => x.CreatedDate);
            ;

            //Build Your Query With All Parameters Here

            query = CreatePagedQuery<T>(query, _params.PageNumber, _params.skip, _params.take);

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetSecondaryAllocationDownloadParams : PagedQueryParamsDtoBridge
    {
        public string? AllocationType { get; set; }
        public int skip { get; set; }
        public int take { get; set; }
    }
}