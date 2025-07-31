using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    ///
    /// </summary>
    public class SearchMyDepositSlips : FlexiQueryPagedListBridgeAsync<PayInSlip, SearchMyDepositSlipsParams, SearchMyDepositSlipsDto, FlexAppContextBridge>
    {
        protected readonly ILogger<SearchMyDepositSlips> _logger;
        protected SearchMyDepositSlipsParams _params;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        private string userId;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public SearchMyDepositSlips(ILogger<SearchMyDepositSlips> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual SearchMyDepositSlips AssignParameters(SearchMyDepositSlipsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<FlexiPagedList<SearchMyDepositSlipsDto>> Fetch()
        {
            _flexAppContext = _params.GetAppContext();  //do not remove this line

            userId = _flexAppContext.UserId;

            var projection = await Build<PayInSlip>().SelectTo<SearchMyDepositSlipsDto>().ToListAsync();

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
                                   .ByloggedInUser(userId)
                                   .DateRange(_params.Datefrom, _params.Dateto)
                                   .GetByCMSPayInSlipNo(_params.PayinSlipNo)
                                   .ByPayInSlipMode(_params.PaymentMode);

            query = CreatePagedQuery<T>(query, _params.PageNumber, _params.skip, _params.take);

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class SearchMyDepositSlipsParams : PagedQueryParamsDtoBridge
    {
        public string? PayinSlipNo { get; set; }

        [Required]
        public DateTime? Datefrom { get; set; }

        [Required]
        public DateTime? Dateto { get; set; }

        [Required]
        public string PaymentMode { get; set; }

        [Required]
        public string ProductGroup { get; set; }

        public int take { get; set; }

        public int skip { get; set; }
    }
}