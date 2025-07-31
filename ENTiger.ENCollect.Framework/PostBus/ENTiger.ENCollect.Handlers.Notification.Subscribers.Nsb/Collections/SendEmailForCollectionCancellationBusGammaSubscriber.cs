using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendEmailForCollectionCancellationBusGammaSubscriber : NsbSubscriberBridge<CollectionCancellationAdded>
    {
        readonly ILogger<SendEmailForCollectionCancellationBusGammaSubscriber> _logger;
        readonly ISendEmailForCollectionCancellation _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendEmailForCollectionCancellationBusGammaSubscriber(ILogger<SendEmailForCollectionCancellationBusGammaSubscriber> logger, ISendEmailForCollectionCancellation subscriber)
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
