using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.FeedbackModule
{
    /// <summary>
    /// 
    /// </summary>
    public class AddTrailsBasedOnCustomerIdBusGammaSubscriber : NsbSubscriberBridge<FeedbackAddedEvent>
    {
        readonly ILogger<AddTrailsBasedOnCustomerIdBusGammaSubscriber> _logger;
        readonly IAddTrails _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public AddTrailsBasedOnCustomerIdBusGammaSubscriber(ILogger<AddTrailsBasedOnCustomerIdBusGammaSubscriber> logger, IAddTrails subscriber)
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
        public override async Task Handle(FeedbackAddedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
