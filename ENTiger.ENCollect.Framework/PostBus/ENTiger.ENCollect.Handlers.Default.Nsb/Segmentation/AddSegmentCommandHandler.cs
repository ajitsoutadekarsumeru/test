using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class AddSegmentCommandHandler : NsbCommandHandler<AddSegmentCommand>
    {
        readonly ILogger<AddSegmentCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public AddSegmentCommandHandler(ILogger<AddSegmentCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(AddSegmentCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing AddSegmentCommandHandler: {nameof(AddSegmentCommandHandler)}");

            await this.ProcessHandlerSequence<AddSegmentPostBusDataPacket, AddSegmentPostBusSequence, 
                AddSegmentCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
