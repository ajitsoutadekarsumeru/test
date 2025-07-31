using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendPaymentLinkViaEmailBusGammaSubscriber : NsbSubscriberBridge<PaymentLinkGeneratedEvent>
    {
        readonly ILogger<SendPaymentLinkViaEmailBusGammaSubscriber> _logger;
        readonly ISendPaymentLinkViaEmail _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendPaymentLinkViaEmailBusGammaSubscriber(ILogger<SendPaymentLinkViaEmailBusGammaSubscriber> logger, ISendPaymentLinkViaEmail subscriber)
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
