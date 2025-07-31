using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendEMailForDepositSlipCreatedBusGammaSubscriber : NsbSubscriberBridge<DepositSlipCreatedEvent>
    {
        readonly ILogger<SendEMailForDepositSlipCreatedBusGammaSubscriber> _logger;
        readonly ISendEMailForDepositSlipCreated _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendEMailForDepositSlipCreatedBusGammaSubscriber(ILogger<SendEMailForDepositSlipCreatedBusGammaSubscriber> logger, ISendEMailForDepositSlipCreated subscriber)
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
        public override async Task Handle(DepositSlipCreatedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
