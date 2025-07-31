using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    /// 
    /// </summary>
    public class agencygetimageCommandHandler : NsbCommandHandler<agencygetimageCommand>
    {
        readonly ILogger<agencygetimageCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public agencygetimageCommandHandler(ILogger<agencygetimageCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(agencygetimageCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing agencygetimageCommandHandler: {nameof(agencygetimageCommandHandler)}");

            await this.ProcessHandlerSequence<agencygetimagePostBusDataPacket, agencygetimagePostBusSequence, 
                agencygetimageCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
