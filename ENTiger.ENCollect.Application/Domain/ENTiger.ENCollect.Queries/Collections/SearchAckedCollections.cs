using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public class SearchAckedCollections : FlexiQueryPagedListBridgeAsync<Collection, SearchAckedCollectionsParams, SearchAckedCollectionsDto, FlexAppContextBridge>
    {
        protected readonly ILogger<SearchAckedCollections> _logger;
        protected SearchAckedCollectionsParams _params;
        protected readonly IRepoFactory _repoFactory;
        protected readonly IFlexHost _flexHost;
        private CollectionWorkflowState _collectionAcknowledged;
        private CollectionWorkflowState _readyForBatch;
        private string ackingAgentId;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public SearchAckedCollections(ILogger<SearchAckedCollections> logger, IRepoFactory repoFactory, IFlexHost flexHost)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _flexHost = flexHost;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual SearchAckedCollections AssignParameters(SearchAckedCollectionsParams @params)
        {
            _params = @params;
            _readyForBatch = _flexHost.GetFlexStateInstance<ReadyForBatch>();
            _collectionAcknowledged = _flexHost.GetFlexStateInstance<CollectionAcknowledged>();
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<FlexiPagedList<SearchAckedCollectionsDto>> Fetch()
        {
            var projection = await Build<Collection>().SelectTo<SearchAckedCollectionsDto>().ToListAsync();

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
                                    .ByPaymentMode(_params.paymentMode)
                                    .ByProductGroup(_params.productGroup)
                                    .ByReceiptType(_params.ReceiptType)
                                    .IncludeCollectionWorkflowState()
                                    .ByReadyForBatchWorkflowState(_readyForBatch, _collectionAcknowledged)
                                    .AckDateRange(_params.fromDate, _params.toDate);

            //Build Your Query With All Parameters Here

            query = CreatePagedQuery<T>(query, _params.PageNumber, _params.skip, _params.take);

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class SearchAckedCollectionsParams : PagedQueryParamsDtoBridge
    {
        [Required]
        public string? productGroup { get; set; }

        [Required]
        public string? paymentMode { get; set; }

        [Required]
        public DateTime fromDate { get; set; }

        [Required]
        public DateTime toDate { get; set; }

        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Invalid CollectorId")]
        public string? collectorId { get; set; }

        public string? ReceiptType { get; set; }
        public int take { get; set; }
        public int skip { get; set; }
    }
}