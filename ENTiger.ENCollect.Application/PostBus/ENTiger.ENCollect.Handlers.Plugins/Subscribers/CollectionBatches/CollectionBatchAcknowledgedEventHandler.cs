using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionBatchesModule
{
    public partial class CollectionBatchAcknowledgedEventHandler : ICollectionBatchAcknowledgedEventHandler
    {
        protected readonly ILogger<CollectionBatchAcknowledgedEventHandler> _logger;
        protected string EventCondition = "";
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;

        public CollectionBatchAcknowledgedEventHandler(ILogger<CollectionBatchAcknowledgedEventHandler> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(CollectionBatchAcknowledgedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext;

            await this.Fire<CollectionBatchAcknowledgedEventHandler>(EventCondition, serviceBusContext);
        }
    }
}