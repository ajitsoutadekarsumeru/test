using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.FeedbackModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetTrailGapDownloadDetails : FlexiQueryEnumerableBridgeAsync<TrailGapDownload, GetTrailGapDownloadDetailsDto>
    {
        protected readonly ILogger<GetTrailGapDownloadDetails> _logger;
        protected GetTrailGapDownloadDetailsParams _params;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        string userId = string.Empty;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetTrailGapDownloadDetails(ILogger<GetTrailGapDownloadDetails> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetTrailGapDownloadDetails AssignParameters(GetTrailGapDownloadDetailsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetTrailGapDownloadDetailsDto>> Fetch()
        {
            _repoFactory.Init(_params);
            _flexAppContext = _params.GetAppContext();
            var userid = _flexAppContext.UserId;

            var result = await Build<TrailGapDownload>().SelectTo<GetTrailGapDownloadDetailsDto>().ToListAsync();

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

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().Where(a => a.CreatedBy == userId).OrderByDescending(x => x.LastModifiedDate);

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetTrailGapDownloadDetailsParams : DtoBridge
    {
    }
}