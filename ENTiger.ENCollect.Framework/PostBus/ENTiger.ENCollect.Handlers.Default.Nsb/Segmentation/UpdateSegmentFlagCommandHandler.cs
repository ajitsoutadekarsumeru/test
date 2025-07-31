using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateSegmentFlagCommandHandler : NsbCommandHandler<UpdateSegmentFlagCommand>
    {
        readonly ILogger<UpdateSegmentFlagCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateSegmentFlagCommandHandler(ILogger<UpdateSegmentFlagCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(UpdateSegmentFlagCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing UpdateSegmentFlagCommandHandler: {nameof(UpdateSegmentFlagCommandHandler)}");

            await this.ProcessHandlerSequence<UpdateSegmentFlagPostBusDataPacket, UpdateSegmentFlagPostBusSequence, 
                UpdateSegmentFlagCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
