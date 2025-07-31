using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendEMailForCollectionCancellationApprovedBusGammaSubscriber : NsbSubscriberBridge<CollectionCancellationApproved>
    {
        readonly ILogger<SendEMailForCollectionCancellationApprovedBusGammaSubscriber> _logger;
        readonly ISendEMailForCollectionCancellationApproved _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendEMailForCollectionCancellationApprovedBusGammaSubscriber(ILogger<SendEMailForCollectionCancellationApprovedBusGammaSubscriber> logger, ISendEMailForCollectionCancellationApproved subscriber)
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
        public override async Task Handle(CollectionCancellationApproved message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
