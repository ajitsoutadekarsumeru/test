using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    ///
    /// </summary>
    public class SearchDepositSlips : FlexiQueryPagedListBridgeAsync<Collection, SearchDepositSlipsParams, SearchDepositSlipsDto, FlexAppContextBridge>
    {
        protected readonly ILogger<SearchDepositSlips> _logger;
        protected SearchDepositSlipsParams _params;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        private IFlexHost _flexHost;
        private CollectionWorkflowState _state;
        private string userId;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public SearchDepositSlips(ILogger<SearchDepositSlips> logger, IRepoFactory repoFactory)
        {
            _flexHost = FlexContainer.ServiceProvider.GetRequiredService<IFlexHost>();
            _logger = logger;
            _repoFactory = repoFactory;
            _state = _flexHost.GetFlexStateInstance<ReceivedByCollector>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual SearchDepositSlips AssignParameters(SearchDepositSlipsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<FlexiPagedList<SearchDepositSlipsDto>> Fetch()
        {
            _flexAppContext = _params.GetAppContext();  //do not remove this line
            userId = _flexAppContext.UserId;

            var projection = await Build<Collection>().SelectTo<SearchDepositSlipsDto>().ToListAsync();

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
                                    .ByAccountNoProductGroup(_params.ProductGroup)
                                    .ByCollectionCollectorId(userId)
                                    .ByPaymentMode(_params.PaymentMode)
                                    .ByWorkflowState(_state)
                                    .CollectionDateRange(_params.Datefrom, _params.Dateto);

            //Build Your Query With All Parameters Here

            query = CreatePagedQuery<T>(query, _params.PageNumber, _params.skip, _params.take);

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class SearchDepositSlipsParams : PagedQueryParamsDtoBridge
    {
        [Required]
        public DateTime? Datefrom { get; set; }

        [Required]
        public DateTime? Dateto { get; set; }

        [Required]
        public string PaymentMode { get; set; }

        [Required]
        public string ProductGroup { get; set; }

        public string? PayinSlipNo { get; set; }
        public int take { get; set; }
        public int skip { get; set; }
    }
}