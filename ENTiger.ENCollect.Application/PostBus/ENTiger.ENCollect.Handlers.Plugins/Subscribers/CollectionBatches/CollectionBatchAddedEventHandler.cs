using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionBatchesModule
{
    public partial class CollectionBatchAddedEventHandler : ICollectionBatchAddedEventHandler
    {
        protected readonly ILogger<CollectionBatchAddedEventHandler> _logger;
        protected string EventCondition = "";
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;

        public CollectionBatchAddedEventHandler(ILogger<CollectionBatchAddedEventHandler> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(CollectionBatchAddedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext;
            await this.Fire<CollectionBatchAddedEventHandler>(EventCondition, serviceBusContext);
        }
    }
}