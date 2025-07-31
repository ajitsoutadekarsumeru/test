using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateToClientForDepositSlipCreatedBusGammaSubscriber : NsbSubscriberBridge<DepositSlipCreatedEvent>
    {
        readonly ILogger<UpdateToClientForDepositSlipCreatedBusGammaSubscriber> _logger;
        readonly IUpdateToClientForDepositSlipCreated _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateToClientForDepositSlipCreatedBusGammaSubscriber(ILogger<UpdateToClientForDepositSlipCreatedBusGammaSubscriber> logger, IUpdateToClientForDepositSlipCreated subscriber)
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
