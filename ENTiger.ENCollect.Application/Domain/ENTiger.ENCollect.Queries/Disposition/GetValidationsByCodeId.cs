using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.DispositionModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetValidationsByCodeId : FlexiQueryEnumerableBridgeAsync<DispositionValidationMaster, GetValidationsByCodeIdDto>
    {
        protected readonly ILogger<GetValidationsByCodeId> _logger;
        protected GetValidationsByCodeIdParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetValidationsByCodeId(ILogger<GetValidationsByCodeId> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetValidationsByCodeId AssignParameters(GetValidationsByCodeIdParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetValidationsByCodeIdDto>> Fetch()
        {
            return await Build<DispositionValidationMaster>().SelectTo<GetValidationsByCodeIdDto>().ToListAsync();
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override IQueryable<T> Build<T>()
        {
            _repoFactory.Init(_params);

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().Where(x => x.DispositionCodeMasterId == _params.DispositionCodeId);

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetValidationsByCodeIdParams : DtoBridge
    {
        public string DispositionCodeId { get; set; }
    }
}