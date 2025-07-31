using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class WalletConsumptionBusGammaSubscriber : NsbSubscriberBridge<CollectionAddedEvent>
    {
        readonly ILogger<WalletConsumptionBusGammaSubscriber> _logger;
        readonly IWalletConsumption _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public WalletConsumptionBusGammaSubscriber(ILogger<WalletConsumptionBusGammaSubscriber> logger, IWalletConsumption subscriber)
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
        public override async Task Handle(CollectionAddedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
