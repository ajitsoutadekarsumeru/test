using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.FeedbackModule
{
    /// <summary>
    ///
    /// </summary>
    public class SearchBulkTrailUploadStatus : FlexiQueryPagedListBridgeAsync<BulkTrailUploadFile, SearchBulkTrailUploadStatusParams, SearchBulkTrailUploadStatusDto, FlexAppContextBridge>
    {
        protected readonly ILogger<SearchBulkTrailUploadStatus> _logger;
        protected SearchBulkTrailUploadStatusParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public SearchBulkTrailUploadStatus(ILogger<SearchBulkTrailUploadStatus> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual SearchBulkTrailUploadStatus AssignParameters(SearchBulkTrailUploadStatusParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<FlexiPagedList<SearchBulkTrailUploadStatusDto>> Fetch()
        {
            var projection = await Build<BulkTrailUploadFile>().SelectTo<SearchBulkTrailUploadStatusDto>().ToListAsync();

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
                .ByBulkCustId(_params.TransactionId)
                           .ByBulkFileName(_params.FileName)
                           .ByBulkUploadedDate(_params.FileuploadDate)
                           .ByBulkFileUploadedStatus(_params.status);

            //Build Your Query With All Parameters Here

            query = CreatePagedQuery<T>(query, _params.PageNumber, _params.skip, _params.take);

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class SearchBulkTrailUploadStatusParams : PagedQueryParamsDtoBridge
    {
        public string? TransactionId { get; set; }

        public string? status { get; set; }

        public string? FileName { get; set; }

        public DateTime? FileuploadDate { get; set; }

        public int skip { get; set; }
        public int take { get; set; }
    }
}