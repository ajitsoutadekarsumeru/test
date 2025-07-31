using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateUserWalletOnPayInSlipAcknowledgedBusGammaSubscriber : NsbSubscriberBridge<PayInSlipAcknowledgedEvent>
    {
        readonly ILogger<UpdateUserWalletOnPayInSlipAcknowledgedBusGammaSubscriber> _logger;
        readonly IUpdateUserWalletOnPayInSlipAcknowledged _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateUserWalletOnPayInSlipAcknowledgedBusGammaSubscriber(ILogger<UpdateUserWalletOnPayInSlipAcknowledgedBusGammaSubscriber> logger, IUpdateUserWalletOnPayInSlipAcknowledged subscriber)
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
        public override async Task Handle(PayInSlipAcknowledgedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
