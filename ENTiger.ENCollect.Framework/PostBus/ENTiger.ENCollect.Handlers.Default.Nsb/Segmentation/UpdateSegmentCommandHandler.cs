using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateSegmentCommandHandler : NsbCommandHandler<UpdateSegmentCommand>
    {
        readonly ILogger<UpdateSegmentCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateSegmentCommandHandler(ILogger<UpdateSegmentCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(UpdateSegmentCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing UpdateSegmentCommandHandler: {nameof(UpdateSegmentCommandHandler)}");

            await this.ProcessHandlerSequence<UpdateSegmentPostBusDataPacket, UpdateSegmentPostBusSequence, 
                UpdateSegmentCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
