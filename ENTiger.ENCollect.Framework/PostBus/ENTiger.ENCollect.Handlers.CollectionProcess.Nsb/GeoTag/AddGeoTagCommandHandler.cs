using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.GeoTagModule
{
    /// <summary>
    /// 
    /// </summary>
    public class AddGeoTagCommandHandler : NsbCommandHandler<AddGeoTagCommand>
    {
        readonly ILogger<AddGeoTagCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public AddGeoTagCommandHandler(ILogger<AddGeoTagCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(AddGeoTagCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing AddGeoTagCommandHandler: {nameof(AddGeoTagCommandHandler)}");

            await this.ProcessHandlerSequence<AddGeoTagPostBusDataPacket, AddGeoTagPostBusSequence, 
                AddGeoTagCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
