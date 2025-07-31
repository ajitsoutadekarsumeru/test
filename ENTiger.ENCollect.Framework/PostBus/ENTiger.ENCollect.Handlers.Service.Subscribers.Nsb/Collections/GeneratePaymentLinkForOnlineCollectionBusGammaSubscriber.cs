using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class GeneratePaymentLinkForOnlineCollectionBusGammaSubscriber : NsbSubscriberBridge<OnlineCollectionAddedEvent>
    {
        readonly ILogger<GeneratePaymentLinkForOnlineCollectionBusGammaSubscriber> _logger;
        readonly IGeneratePaymentLinkForOnlineCollection _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public GeneratePaymentLinkForOnlineCollectionBusGammaSubscriber(ILogger<GeneratePaymentLinkForOnlineCollectionBusGammaSubscriber> logger, IGeneratePaymentLinkForOnlineCollection subscriber)
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
        public override async Task Handle(OnlineCollectionAddedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
