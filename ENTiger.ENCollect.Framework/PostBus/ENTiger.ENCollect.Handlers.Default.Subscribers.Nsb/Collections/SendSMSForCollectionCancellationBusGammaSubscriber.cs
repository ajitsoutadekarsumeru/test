using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendSMSForCollectionCancellationBusGammaSubscriber : NsbSubscriberBridge<CollectionCancellationAdded>
    {
        readonly ILogger<SendSMSForCollectionCancellationBusGammaSubscriber> _logger;
        readonly ISendSMSForCollectionCancellation _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendSMSForCollectionCancellationBusGammaSubscriber(ILogger<SendSMSForCollectionCancellationBusGammaSubscriber> logger, ISendSMSForCollectionCancellation subscriber)
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
        public override async Task Handle(CollectionCancellationAdded message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
