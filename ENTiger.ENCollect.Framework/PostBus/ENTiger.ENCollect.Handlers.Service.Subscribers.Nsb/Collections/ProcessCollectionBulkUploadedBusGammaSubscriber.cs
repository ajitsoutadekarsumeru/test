using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class ProcessCollectionBulkUploadedBusGammaSubscriber : NsbSubscriberBridge<CollectionBulkUploadedEvent>
    {
        readonly ILogger<ProcessCollectionBulkUploadedBusGammaSubscriber> _logger;
        readonly IProcessCollectionBulkUploaded _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ProcessCollectionBulkUploadedBusGammaSubscriber(ILogger<ProcessCollectionBulkUploadedBusGammaSubscriber> logger, IProcessCollectionBulkUploaded subscriber)
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
        public override async Task Handle(CollectionBulkUploadedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
