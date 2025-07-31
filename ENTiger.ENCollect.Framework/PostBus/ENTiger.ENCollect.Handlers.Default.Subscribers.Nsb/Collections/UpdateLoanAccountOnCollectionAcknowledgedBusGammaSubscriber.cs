using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateLoanAccountOnCollectionAcknowledgedBusGammaSubscriber : NsbSubscriberBridge<CollectionAcknowledgedEvent>
    {
        readonly ILogger<UpdateLoanAccountOnCollectionAcknowledgedBusGammaSubscriber> _logger;
        readonly IUpdateLoanAccountOnCollectionAcknowledged _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateLoanAccountOnCollectionAcknowledgedBusGammaSubscriber(ILogger<UpdateLoanAccountOnCollectionAcknowledgedBusGammaSubscriber> logger, IUpdateLoanAccountOnCollectionAcknowledged subscriber)
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
        public override async Task Handle(CollectionAcknowledgedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
