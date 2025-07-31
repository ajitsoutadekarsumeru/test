using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class DisableSegmentsCommandHandler : NsbCommandHandler<DisableSegmentsCommand>
    {
        readonly ILogger<DisableSegmentsCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public DisableSegmentsCommandHandler(ILogger<DisableSegmentsCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(DisableSegmentsCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing DisableSegmentsCommandHandler: {nameof(DisableSegmentsCommandHandler)}");

            await this.ProcessHandlerSequence<DisableSegmentsPostBusDataPacket, DisableSegmentsPostBusSequence, 
                DisableSegmentsCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
