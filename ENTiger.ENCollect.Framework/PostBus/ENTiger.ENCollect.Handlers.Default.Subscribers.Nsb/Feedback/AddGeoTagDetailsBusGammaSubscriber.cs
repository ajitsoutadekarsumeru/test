using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.FeedbackModule
{
    /// <summary>
    /// 
    /// </summary>
    public class AddGeoTagDetailsBusGammaSubscriber : NsbSubscriberBridge<FeedbackAddedEvent>
    {
        readonly ILogger<AddGeoTagDetailsBusGammaSubscriber> _logger;
        readonly IAddGeoTagDetails _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public AddGeoTagDetailsBusGammaSubscriber(ILogger<AddGeoTagDetailsBusGammaSubscriber> logger, IAddGeoTagDetails subscriber)
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
