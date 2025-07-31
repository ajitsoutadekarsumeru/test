using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendEmailForCollectionBulkUploadProcessedBusGammaSubscriber : NsbSubscriberBridge<CollectionBulkUploadProcessedEvent>
    {
        readonly ILogger<SendEmailForCollectionBulkUploadProcessedBusGammaSubscriber> _logger;
        readonly ISendEmailForCollectionBulkUploadProcessed _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendEmailForCollectionBulkUploadProcessedBusGammaSubscriber(ILogger<SendEmailForCollectionBulkUploadProcessedBusGammaSubscriber> logger, ISendEmailForCollectionBulkUploadProcessed subscriber)
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
        public override async Task Handle(CollectionBulkUploadProcessedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
