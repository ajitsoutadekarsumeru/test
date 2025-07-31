using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetPrimaryAllocationDownload : FlexiQueryPagedListBridgeAsync<AllocationDownload, GetPrimaryAllocationDownloadParams, GetPrimaryAllocationDownloadDto, FlexAppContextBridge>
    {
        protected readonly ILogger<GetPrimaryAllocationDownload> _logger;
        protected GetPrimaryAllocationDownloadParams _params;
        protected readonly IRepoFactory _repoFactory;
        private string? loggedInUserId;
        protected FlexAppContextBridge? _flexAppContext;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetPrimaryAllocationDownload(ILogger<GetPrimaryAllocationDownload> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetPrimaryAllocationDownload AssignParameters(GetPrimaryAllocationDownloadParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<FlexiPagedList<GetPrimaryAllocationDownloadDto>> Fetch()
        {
            _flexAppContext = _params.GetAppContext();
            loggedInUserId = _flexAppContext.UserId;

            var projection = await Build<AllocationDownload>().SelectTo<GetPrimaryAllocationDownloadDto>().ToListAsync();

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

            //Build Your Query With All Parameters Here

            query = CreatePagedQuery<T>(query, _params.PageNumber, _params.Skip, _params.take);

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetPrimaryAllocationDownloadParams : PagedQueryParamsDtoBridge
    {
        public string? AllocationType { get; set; }
        public int take { get; set; }
    }
}