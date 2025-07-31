using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetUnsequencedAutoSegments : FlexiQueryEnumerableBridgeAsync<Segmentation, GetUnsequencedAutoSegmentsDto>
    {
        protected readonly ILogger<GetUnsequencedAutoSegments> _logger;
        protected GetUnsequencedAutoSegmentsParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetUnsequencedAutoSegments(ILogger<GetUnsequencedAutoSegments> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetUnsequencedAutoSegments AssignParameters(GetUnsequencedAutoSegmentsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetUnsequencedAutoSegmentsDto>> Fetch()
        {
            return await Build<Segmentation>().SelectTo<GetUnsequencedAutoSegmentsDto>().ToListAsync();
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override IQueryable<T> Build<T>()
        {
            _repoFactory.Init(_params);

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().Where(a => a.IsDeleted != true && a.IsDisabled != true && string.Equals(a.ExecutionType, "auto") && (a.Sequence == null || a.Sequence == 0));

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetUnsequencedAutoSegmentsParams : DtoBridge
    {
    }
}