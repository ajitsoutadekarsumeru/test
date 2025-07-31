using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetCollectionsForCancellation : FlexiQueryPagedListBridgeAsync<Collection, GetCollectionsForCancellationParams, GetCollectionsForCancellationDto, FlexAppContextBridge>
    {
        protected readonly ILogger<GetCollectionsForCancellation> _logger;
        protected GetCollectionsForCancellationParams _params;
        protected readonly IRepoFactory _repoFactory;
        private CollectionWorkflowState receivebycollector = new ReceivedByCollector();
        private CollectionWorkflowState ReadyForBatch = new ReadyForBatch();
        private CollectionWorkflowState ErrorInCBS = new ErrorInCBS();
        private CollectionWorkflowState CollectionAcknowledged = new CollectionAcknowledged();
        protected FlexAppContextBridge? _flexAppContext;
        private string userId;
        private string tenantId;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetCollectionsForCancellation(ILogger<GetCollectionsForCancellation> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetCollectionsForCancellation AssignParameters(GetCollectionsForCancellationParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<FlexiPagedList<GetCollectionsForCancellationDto>> Fetch()
        {
            _flexAppContext = _params.GetAppContext();/*Cmd.Dto.GetAppContext();*/
            userId = _flexAppContext.UserId;
            tenantId = _flexAppContext.TenantId;

            var projection = await Build<Collection>().SelectTo<GetCollectionsForCancellationDto>().ToListAsync();

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
                                    .CollectionForCancellationWorkFlowState(receivebycollector, CollectionAcknowledged, ReadyForBatch, ErrorInCBS)
                                    .ByCollectionAccountNo(_params.AgreementId)
                                    .CollectionDateRange(_params.ReceiptFromDate, _params.ReceiptToDate)
                                    .OrderByDescending(x => x.CreatedDate);

            //Build Your Query With All Parameters Here

            query = CreatePagedQuery<T>(query, _params.PageNumber, _params.skip, _params.take);

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetCollectionsForCancellationParams : PagedQueryParamsDtoBridge
    {
        public string? AgreementId { get; set; }

        public DateTime? ReceiptFromDate { get; set; }

        public DateTime? ReceiptToDate { get; set; }

        public int take { get; set; }

        public int skip { get; set; }
    }
}