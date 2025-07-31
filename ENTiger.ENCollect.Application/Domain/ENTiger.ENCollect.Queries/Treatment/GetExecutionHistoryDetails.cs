using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetExecutionHistoryDetails : FlexiQueryEnumerableBridgeAsync<TreatmentHistory, GetExecutionHistoryDetailsDto>
    {
        protected readonly ILogger<GetExecutionHistoryDetails> _logger;
        protected GetExecutionHistoryDetailsParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetExecutionHistoryDetails(ILogger<GetExecutionHistoryDetails> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetExecutionHistoryDetails AssignParameters(GetExecutionHistoryDetailsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetExecutionHistoryDetailsDto>> Fetch()
        {
            var result = await Build<TreatmentHistory>().SelectTo<GetExecutionHistoryDetailsDto>().ToListAsync();

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
                                    .Where(x => x.TreatmentId == _params.TreatmentId);

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetExecutionHistoryDetailsParams : DtoBridge
    {
        public string TreatmentId { get; set; }
    }
}