using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.PublicModule
{
    /// <summary>
    /// 
    /// </summary>
    public class ProcessCollectionImportedBusGammaSubscriber : NsbSubscriberBridge<CollectionImportedEvent>
    {
        readonly ILogger<ProcessCollectionImportedBusGammaSubscriber> _logger;
        readonly IProcessCollectionImported _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ProcessCollectionImportedBusGammaSubscriber(ILogger<ProcessCollectionImportedBusGammaSubscriber> logger, IProcessCollectionImported subscriber)
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
        public override async Task Handle(CollectionImportedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
