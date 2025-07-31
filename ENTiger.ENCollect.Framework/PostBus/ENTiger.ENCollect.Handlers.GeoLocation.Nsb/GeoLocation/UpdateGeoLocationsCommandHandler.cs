using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.GeoLocationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateGeoLocationsCommandHandler : NsbCommandHandler<UpdateGeoLocationsCommand>
    {
        readonly ILogger<UpdateGeoLocationsCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateGeoLocationsCommandHandler(ILogger<UpdateGeoLocationsCommandHandler> logger, IFlexHost flexHost)
        {
            _logger = logger;
            _flexHost = flexHost;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task Handle(UpdateGeoLocationsCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing UpdateGeoLocationsCommandHandler: {nameof(UpdateGeoLocationsCommandHandler)}");

            await this.ProcessHandlerSequence<UpdateGeoLocationsPostBusDataPacket, UpdateGeoLocationsPostBusSequence, 
                UpdateGeoLocationsCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
