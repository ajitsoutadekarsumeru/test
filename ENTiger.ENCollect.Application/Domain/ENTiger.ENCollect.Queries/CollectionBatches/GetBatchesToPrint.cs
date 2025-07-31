using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionBatchesModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetBatchesToPrint : FlexiQueryEnumerableBridgeAsync<CollectionBatch, GetBatchesToPrintDto>
    {
        protected readonly ILogger<GetBatchesToPrint> _logger;
        protected GetBatchesToPrintParams _params;
        protected readonly IRepoFactory _repoFactory;
        private CollectionBatchWorkflowState _batchcreated;
        private CollectionBatchWorkflowState _Ackbatch;
        private string userId = string.Empty;
        private string _orgid;
        protected FlexAppContextBridge? _flexAppContext;
        private IFlexHost _flexHost;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetBatchesToPrint(ILogger<GetBatchesToPrint> logger, IRepoFactory repoFactory)
        {
            _flexHost = FlexContainer.ServiceProvider.GetRequiredService<IFlexHost>();
            _logger = logger;
            _repoFactory = repoFactory;
            _batchcreated = _flexHost.GetFlexStateInstance<CollectionBatchCreated>();
            _Ackbatch = _flexHost.GetFlexStateInstance<CollectionBatchAcknowledged>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetBatchesToPrint AssignParameters(GetBatchesToPrintParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetBatchesToPrintDto>> Fetch()
        {
            _flexAppContext = _params.GetAppContext();  //do not remove this line
            userId = _flexAppContext.UserId;

            _repoFactory.Init(_params);

            var accountability = await _repoFactory.GetRepo().FindAll<Accountability>().Where(a => a.ResponsibleId == userId).FirstOrDefaultAsync();
            _orgid = accountability?.CommisionerId;

            var result = await Build<CollectionBatch>().SelectTo<GetBatchesToPrintDto>().ToListAsync();

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
                                    .FlexInclude(x => x.CollectionBatchWorkflowState)
                                    .ByBatchCreatedAndAckWorkFLowState(_batchcreated, _Ackbatch)
                                    .Where(i => i.CollectionBatchOrgId == _orgid);
            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetBatchesToPrintParams : DtoBridge
    {
    }
}