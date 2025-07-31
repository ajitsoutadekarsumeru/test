using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class PrimaryUnAllocationByBatchCommandHandler : NsbCommandHandler<PrimaryUnAllocationByBatchCommand>
    {
        readonly ILogger<PrimaryUnAllocationByBatchCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public PrimaryUnAllocationByBatchCommandHandler(ILogger<PrimaryUnAllocationByBatchCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(PrimaryUnAllocationByBatchCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing PrimaryUnAllocationByBatchCommandHandler: {nameof(PrimaryUnAllocationByBatchCommandHandler)}");

            await this.ProcessHandlerSequence<PrimaryUnAllocationByBatchPostBusDataPacket, PrimaryUnAllocationByBatchPostBusSequence, 
                PrimaryUnAllocationByBatchCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
