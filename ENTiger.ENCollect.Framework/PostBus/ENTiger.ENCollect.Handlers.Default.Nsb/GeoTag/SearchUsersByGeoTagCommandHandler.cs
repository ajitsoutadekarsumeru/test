using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.GeoTagModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SearchUsersByGeoTagCommandHandler : NsbCommandHandler<SearchUsersByGeoTagCommand>
    {
        readonly ILogger<SearchUsersByGeoTagCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SearchUsersByGeoTagCommandHandler(ILogger<SearchUsersByGeoTagCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(SearchUsersByGeoTagCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing SearchUsersByGeoTagCommandHandler: {nameof(SearchUsersByGeoTagCommandHandler)}");

            await this.ProcessHandlerSequence<SearchUsersByGeoTagPostBusDataPacket, SearchUsersByGeoTagPostBusSequence, 
                SearchUsersByGeoTagCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
