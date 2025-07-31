using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.GeoLocationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateUserGeoLocationBusGammaSubscriber : NsbSubscriberBridge<UserGeoLocationUpdateRequested>
    {
        readonly ILogger<UpdateUserGeoLocationBusGammaSubscriber> _logger;
        readonly IUpdateUserGeoLocation _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateUserGeoLocationBusGammaSubscriber(ILogger<UpdateUserGeoLocationBusGammaSubscriber> logger, IUpdateUserGeoLocation subscriber)
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
        public override async Task Handle(UserGeoLocationUpdateRequested message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
