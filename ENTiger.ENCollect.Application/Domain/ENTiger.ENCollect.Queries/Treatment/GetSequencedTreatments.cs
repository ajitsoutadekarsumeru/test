using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetSequencedTreatments : FlexiQueryEnumerableBridgeAsync<Treatment, GetSequencedTreatmentsDto>
    {
        protected readonly ILogger<GetSequencedTreatments> _logger;
        protected GetSequencedTreatmentsParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetSequencedTreatments(ILogger<GetSequencedTreatments> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetSequencedTreatments AssignParameters(GetSequencedTreatmentsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetSequencedTreatmentsDto>> Fetch()
        {
            var result = await Build<Treatment>().SelectTo<GetSequencedTreatmentsDto>().ToListAsync();

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
                                    .Where(a => a.IsDeleted != true && a.IsDisabled != true && a.Sequence != null)
                                    .OrderBy(a => a.Sequence);

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetSequencedTreatmentsParams : DtoBridge
    {
    }
}