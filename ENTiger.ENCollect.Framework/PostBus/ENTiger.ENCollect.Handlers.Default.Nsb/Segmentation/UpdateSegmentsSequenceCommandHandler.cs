using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateSegmentsSequenceCommandHandler : NsbCommandHandler<UpdateSegmentsSequenceCommand>
    {
        readonly ILogger<UpdateSegmentsSequenceCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateSegmentsSequenceCommandHandler(ILogger<UpdateSegmentsSequenceCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(UpdateSegmentsSequenceCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing UpdateSegmentsSequenceCommandHandler: {nameof(UpdateSegmentsSequenceCommandHandler)}");

            await this.ProcessHandlerSequence<UpdateSegmentsSequencePostBusDataPacket, UpdateSegmentsSequencePostBusSequence, 
                UpdateSegmentsSequenceCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
