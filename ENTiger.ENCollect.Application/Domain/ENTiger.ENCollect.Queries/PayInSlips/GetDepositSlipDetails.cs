using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetDepositSlipDetails : FlexiQueryBridgeAsync<Collection, GetDepositSlipDetailsDto>
    {
        protected readonly ILogger<GetDepositSlipDetails> _logger;
        protected GetDepositSlipDetailsParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetDepositSlipDetails(ILogger<GetDepositSlipDetails> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual GetDepositSlipDetails AssignParameters(GetDepositSlipDetailsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<GetDepositSlipDetailsDto> Fetch()
        {
            return await Build<Collection>().SelectTo<GetDepositSlipDetailsDto>().FirstOrDefaultAsync();
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override IQueryable<T> Build<T>()
        {
            _repoFactory.Init(_params);

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().ByReceiptNo(_params.ReceiptNo);
            return query;
        }
    }

    public class GetDepositSlipDetailsParams : DtoBridge
    {
        public string ReceiptNo { get; set; }
    }
}