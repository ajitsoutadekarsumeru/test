using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateUserWalletOnCollectionAddedBusGammaSubscriber : NsbSubscriberBridge<CollectionAddedEvent>
    {
        readonly ILogger<UpdateUserWalletOnCollectionAddedBusGammaSubscriber> _logger;
        readonly IUpdateUserWalletOnCollectionAdded _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateUserWalletOnCollectionAddedBusGammaSubscriber(ILogger<UpdateUserWalletOnCollectionAddedBusGammaSubscriber> logger, IUpdateUserWalletOnCollectionAdded subscriber)
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
