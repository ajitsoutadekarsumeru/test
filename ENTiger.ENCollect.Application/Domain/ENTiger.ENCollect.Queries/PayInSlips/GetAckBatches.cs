using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetAckBatches : FlexiQueryEnumerableBridgeAsync<CollectionBatch, GetAckBatchesDto>
    {
        protected readonly ILogger<GetAckBatches> _logger;
        protected GetAckBatchesParams _params;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        private string userId;
        private string organizationId = string.Empty;
        private CollectionBatchWorkflowState _state;
        private CollectionBatchWorkflowState _AckBatch;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetAckBatches(ILogger<GetAckBatches> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _state = WorkflowStateFactory.GetCollectionBatchWorkflowState("BatchCreated");
            _AckBatch = WorkflowStateFactory.GetCollectionBatchWorkflowState("BatchAcknowledged");
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetAckBatches AssignParameters(GetAckBatchesParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetAckBatchesDto>> Fetch()
        {
            _repoFactory.Init(_params);
            _flexAppContext = _params.GetAppContext();  //do not remove this line
            userId = _flexAppContext.UserId;

            Accountability accountability = await _repoFactory.GetRepo().FindAll<Accountability>().Where(x => x.ResponsibleId == userId).FirstOrDefaultAsync();
            organizationId = accountability?.CommisionerId;

            var result = await Build<CollectionBatch>().SelectTo<GetAckBatchesDto>().ToListAsync();

            var userIds = result.Select(x => x.ReceiptIssuedBy).Distinct().ToArray();

            var users = await _repoFactory.GetRepo().FindAll<ApplicationUser>().Where(x => userIds.Contains(x.Id)).ToListAsync();
            foreach (var obj in result)
            {
                var user = users.Where(x => x.Id == obj.ReceiptIssuedBy).FirstOrDefault();
                obj.ReceiptIssuedBy = user != null ? user.FirstName + " " + user.LastName : string.Empty;
            }

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override IQueryable<T> Build<T>()
        {
            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>()
                                    .ByCollectionBatchOrgId(organizationId)
                                    .ByCollectionBatchPaymentMode(_params.paymentMode)
                                    .ByCollectionBatchType(_params.batchType)
                                    .ByBatchCreatedAndAckWorkFLowState(_state, _AckBatch)
                                    .FlexInclude(x => x.Collections);

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetAckBatchesParams : DtoBridge
    {
        public string? paymentMode { get; set; }
        public string? batchType { get; set; }
        public string? productGroup { get; set; }
    }
}