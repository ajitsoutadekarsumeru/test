using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendPaymentLinkViaSMSBusGammaSubscriber : NsbSubscriberBridge<PaymentLinkGeneratedEvent>
    {
        readonly ILogger<SendPaymentLinkViaSMSBusGammaSubscriber> _logger;
        readonly ISendPaymentLinkViaSMS _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendPaymentLinkViaSMSBusGammaSubscriber(ILogger<SendPaymentLinkViaSMSBusGammaSubscriber> logger, ISendPaymentLinkViaSMS subscriber)
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
        public override async Task Handle(PaymentLinkGeneratedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
