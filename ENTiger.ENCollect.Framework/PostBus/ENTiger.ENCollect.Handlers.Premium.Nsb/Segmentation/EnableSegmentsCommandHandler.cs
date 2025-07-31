using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class EnableSegmentsCommandHandler : NsbCommandHandler<EnableSegmentsCommand>
    {
        readonly ILogger<EnableSegmentsCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public EnableSegmentsCommandHandler(ILogger<EnableSegmentsCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(EnableSegmentsCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing EnableSegmentsCommandHandler: {nameof(EnableSegmentsCommandHandler)}");

            await this.ProcessHandlerSequence<EnableSegmentsPostBusDataPacket, EnableSegmentsPostBusSequence, 
                EnableSegmentsCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
