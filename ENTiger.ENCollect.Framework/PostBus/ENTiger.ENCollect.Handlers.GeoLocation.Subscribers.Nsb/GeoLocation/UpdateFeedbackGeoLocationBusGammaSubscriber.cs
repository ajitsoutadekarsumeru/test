using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.GeoLocationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateFeedbackGeoLocationBusGammaSubscriber : NsbSubscriberBridge<FeedbackGeoLocationUpdateRequested>
    {
        readonly ILogger<UpdateFeedbackGeoLocationBusGammaSubscriber> _logger;
        readonly IUpdateFeedbackGeoLocation _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateFeedbackGeoLocationBusGammaSubscriber(ILogger<UpdateFeedbackGeoLocationBusGammaSubscriber> logger, IUpdateFeedbackGeoLocation subscriber)
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
        public override async Task Handle(FeedbackGeoLocationUpdateRequested message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
