using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionBatchesModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetBatchById : FlexiQueryBridgeAsync<CollectionBatch, GetBatchByIdDto>
    {
        protected readonly ILogger<GetBatchById> _logger;
        protected GetBatchByIdParams _params;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        private CollectionBatchWorkflowState _state;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetBatchById(ILogger<GetBatchById> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetBatchById AssignParameters(GetBatchByIdParams @params)
        {
            _params = @params;
            _state = WorkflowStateFactory.GetCollectionBatchWorkflowState("BatchCreated");
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<GetBatchByIdDto> Fetch()
        {
            GetBatchByIdDto outputModel = new GetBatchByIdDto();
            ICollection<CollectionDetailsOutputApiModel> model = new List<CollectionDetailsOutputApiModel>();

            var collectionBatch = await Build<CollectionBatch>().SelectTo<CollectionBatchOutputAPIModel>().FirstOrDefaultAsync();

            if (collectionBatch != null)
            {
                var batchCollections = await _repoFactory.GetRepo().FindAll<Collection>()
                                    .Where(x => x.CollectionBatchId == collectionBatch.Id).FlexInclude(x => x.Account)
                                    .SelectTo<CollectionDetailsOutputApiModel>().ToListAsync();

                foreach (var collections in batchCollections)
                {
                    CollectionDetailsOutputApiModel apiModel = collections;
                    model.Add(apiModel);
                }
            }
            outputModel.CollectionBatch = collectionBatch;
            outputModel.CollectionDetails = model;

            return outputModel;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override IQueryable<T> Build<T>()
        {
            _repoFactory.Init(_params);
            _flexAppContext = _params.GetAppContext();  //do not remove this line
            string userId = _flexAppContext.UserId;

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>()
                                    .ByBatchCustomId(_params.BatchId)
                                    .FlexInclude(a => a.CollectionBatchWorkflowState)
                                    .ByBatchCreatedWorkFLowState(_state)
                                    .FlexInclude(a => a.AcknowledgedBy);

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetBatchByIdParams : DtoBridge
    {
        public string? BatchId { get; set; }
    }
}