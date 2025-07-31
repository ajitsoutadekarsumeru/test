using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendEmailForCollectionBulkUploadFailedBusGammaSubscriber : NsbSubscriberBridge<CollectionBulkUploadFailedEvent>
    {
        readonly ILogger<SendEmailForCollectionBulkUploadFailedBusGammaSubscriber> _logger;
        readonly ISendEmailForCollectionBulkUploadFailed _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendEmailForCollectionBulkUploadFailedBusGammaSubscriber(ILogger<SendEmailForCollectionBulkUploadFailedBusGammaSubscriber> logger, ISendEmailForCollectionBulkUploadFailed subscriber)
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
        public override async Task Handle(CollectionBulkUploadFailedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
