using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.GeoLocationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateCollectionGeoLocationBusGammaSubscriber : NsbSubscriberBridge<CollectionGeoLocationUpdateRequested>
    {
        readonly ILogger<UpdateCollectionGeoLocationBusGammaSubscriber> _logger;
        readonly IUpdateCollectionGeoLocation _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateCollectionGeoLocationBusGammaSubscriber(ILogger<UpdateCollectionGeoLocationBusGammaSubscriber> logger, IUpdateCollectionGeoLocation subscriber)
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
        public override async Task Handle(CollectionGeoLocationUpdateRequested message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
