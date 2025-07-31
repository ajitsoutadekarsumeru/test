using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetUnSequencedTreatments : FlexiQueryEnumerableBridgeAsync<Treatment, GetUnSequencedTreatmentsDto>
    {
        protected readonly ILogger<GetUnSequencedTreatments> _logger;
        protected GetUnSequencedTreatmentsParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetUnSequencedTreatments(ILogger<GetUnSequencedTreatments> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetUnSequencedTreatments AssignParameters(GetUnSequencedTreatmentsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetUnSequencedTreatmentsDto>> Fetch()
        {
            return await Build<Treatment>().SelectTo<GetUnSequencedTreatmentsDto>().ToListAsync();
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
                                    .Where(a => a.IsDeleted == false && (a.IsDisabled == false || a.IsDisabled == null)
                                        && string.Equals(a.Mode, "auto") && (a.Sequence == null || a.Sequence == 0));

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetUnSequencedTreatmentsParams : DtoBridge
    {
    }
}