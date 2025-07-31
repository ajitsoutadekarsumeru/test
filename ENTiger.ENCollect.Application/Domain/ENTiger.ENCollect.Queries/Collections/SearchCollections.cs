using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public class SearchCollections : FlexiQueryPagedListBridgeAsync<Collection, SearchCollectionsParams, SearchCollectionsDto, FlexAppContextBridge>
    {
        protected readonly ILogger<SearchCollections> _logger;
        protected SearchCollectionsParams _params;
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
        public SearchCollections(ILogger<SearchCollections> logger, IRepoFactory repoFactory)
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
        public virtual SearchCollections AssignParameters(SearchCollectionsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<FlexiPagedList<SearchCollectionsDto>> Fetch()
        {
            _flexAppContext = _params.GetAppContext();
            userId = _flexAppContext.UserId;

            var projection = await Build<Collection>().SelectTo<SearchCollectionsDto>().ToListAsync();

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
                                    .ByCollector(_params.collectorId)
                                    .CollectionDateRange(_params.fromDate, _params.toDate)
                                    .ByWorkflowState(_state);

            //Build Your Query With All Parameters Here

            query = CreatePagedQuery<T>(query, _params.PageNumber, _params.skip, _params.take);

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class SearchCollectionsParams : PagedQueryParamsDtoBridge
    {
        [Required]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Invalid CollectorId")]
        public string collectorId { get; set; }

        [Required]
        public DateTime fromDate { get; set; }
        [Required]
        public DateTime toDate { get; set; }

        public int take { get; set; }

        public int skip { get; set; }
    }
}