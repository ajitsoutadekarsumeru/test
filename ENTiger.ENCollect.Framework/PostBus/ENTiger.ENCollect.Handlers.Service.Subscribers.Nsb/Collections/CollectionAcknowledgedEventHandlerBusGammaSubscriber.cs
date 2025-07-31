using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class CollectionAcknowledgedEventHandlerBusGammaSubscriber : NsbSubscriberBridge<CollectionAcknowledgedEvent>
    {
        readonly ILogger<CollectionAcknowledgedEventHandlerBusGammaSubscriber> _logger;
        readonly ICollectionAcknowledgedEventHandler _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public CollectionAcknowledgedEventHandlerBusGammaSubscriber(ILogger<CollectionAcknowledgedEventHandlerBusGammaSubscriber> logger, ICollectionAcknowledgedEventHandler subscriber)
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
        public override async Task Handle(CollectionAcknowledgedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
