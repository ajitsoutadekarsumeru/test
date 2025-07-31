using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class ExecuteSegmentCommandHandler : NsbCommandHandler<ExecuteSegmentCommand>
    {
        readonly ILogger<ExecuteSegmentCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ExecuteSegmentCommandHandler(ILogger<ExecuteSegmentCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(ExecuteSegmentCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing ExecuteSegmentCommandHandler: {nameof(ExecuteSegmentCommandHandler)}");

            await this.ProcessHandlerSequence<ExecuteSegmentPostBusDataPacket, ExecuteSegmentPostBusSequence, 
                ExecuteSegmentCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
