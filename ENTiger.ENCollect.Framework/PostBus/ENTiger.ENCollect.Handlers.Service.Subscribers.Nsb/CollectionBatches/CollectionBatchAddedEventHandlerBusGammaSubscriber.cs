using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CollectionBatchesModule
{
    /// <summary>
    /// 
    /// </summary>
    public class CollectionBatchAddedEventHandlerBusGammaSubscriber : NsbSubscriberBridge<CollectionBatchAddedEvent>
    {
        readonly ILogger<CollectionBatchAddedEventHandlerBusGammaSubscriber> _logger;
        readonly ICollectionBatchAddedEventHandler _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public CollectionBatchAddedEventHandlerBusGammaSubscriber(ILogger<CollectionBatchAddedEventHandlerBusGammaSubscriber> logger, ICollectionBatchAddedEventHandler subscriber)
        {
            _logger = logger;
            _subscriber = subscriber;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task Handle(CollectionBatchAddedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
