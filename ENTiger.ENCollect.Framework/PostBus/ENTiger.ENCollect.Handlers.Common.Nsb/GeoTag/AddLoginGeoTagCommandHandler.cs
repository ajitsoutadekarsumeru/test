using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.GeoTagModule
{
    /// <summary>
    /// 
    /// </summary>
    public class AddLoginGeoTagCommandHandler : NsbCommandHandler<AddLoginGeoTagCommand>
    {
        readonly ILogger<AddLoginGeoTagCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public AddLoginGeoTagCommandHandler(ILogger<AddLoginGeoTagCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(AddLoginGeoTagCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing AddLoginGeoTagCommandHandler: {nameof(AddLoginGeoTagCommandHandler)}");

            await this.ProcessHandlerSequence<AddLoginGeoTagPostBusDataPacket, AddLoginGeoTagPostBusSequence, 
                AddLoginGeoTagCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
