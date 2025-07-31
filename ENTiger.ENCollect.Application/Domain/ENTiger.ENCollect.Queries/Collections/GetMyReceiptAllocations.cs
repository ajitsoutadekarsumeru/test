using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetMyReceiptAllocations : FlexiQueryEnumerableBridgeAsync<Receipt, GetMyReceiptAllocationsDto>
    {
        protected readonly ILogger<GetMyReceiptAllocations> _logger;
        protected GetMyReceiptAllocationsParams _params;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        private ReceiptWorkflowState _state;
        private string userId;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetMyReceiptAllocations(ILogger<GetMyReceiptAllocations> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetMyReceiptAllocations AssignParameters(GetMyReceiptAllocationsParams @params)
        {
            IFlexHost host = FlexContainer.ServiceProvider.GetRequiredService<IFlexHost>();
            _params = @params;
            _state = host.GetFlexStateInstance<ReceiptAllocatedToCollector>();
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetMyReceiptAllocationsDto>> Fetch()
        {
            _flexAppContext = _params.GetAppContext();
            userId = _flexAppContext.UserId;

            var result = await Build<Receipt>().SelectTo<GetMyReceiptAllocationsDto>().ToListAsync();

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

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().ByReceiptCollector(userId).ReceiptWorkflowState(_state);

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetMyReceiptAllocationsParams : DtoBridge
    {
    }
}