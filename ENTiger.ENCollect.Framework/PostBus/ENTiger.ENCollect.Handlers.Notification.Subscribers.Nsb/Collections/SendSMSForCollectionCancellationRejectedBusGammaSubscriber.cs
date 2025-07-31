using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendSMSForCollectionCancellationRejectedBusGammaSubscriber : NsbSubscriberBridge<CollectionCancellationRejected>
    {
        readonly ILogger<SendSMSForCollectionCancellationRejectedBusGammaSubscriber> _logger;
        readonly ISendSMSForCollectionCancellationRejected _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendSMSForCollectionCancellationRejectedBusGammaSubscriber(ILogger<SendSMSForCollectionCancellationRejectedBusGammaSubscriber> logger, ISendSMSForCollectionCancellationRejected subscriber)
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
        public override async Task Handle(CollectionCancellationRejected message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
