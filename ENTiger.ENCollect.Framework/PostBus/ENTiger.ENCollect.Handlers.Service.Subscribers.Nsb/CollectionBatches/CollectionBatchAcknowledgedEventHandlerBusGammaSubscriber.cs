using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CollectionBatchesModule
{
    /// <summary>
    /// 
    /// </summary>
    public class CollectionBatchAcknowledgedEventHandlerBusGammaSubscriber : NsbSubscriberBridge<CollectionBatchAcknowledgedEvent>
    {
        readonly ILogger<CollectionBatchAcknowledgedEventHandlerBusGammaSubscriber> _logger;
        readonly ICollectionBatchAcknowledgedEventHandler _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public CollectionBatchAcknowledgedEventHandlerBusGammaSubscriber(ILogger<CollectionBatchAcknowledgedEventHandlerBusGammaSubscriber> logger, ICollectionBatchAcknowledgedEventHandler subscriber)
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
        public override async Task Handle(CollectionBatchAcknowledgedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
