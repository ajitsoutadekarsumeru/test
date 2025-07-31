using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class PrimaryAllocationByBatchCommandHandler : NsbCommandHandler<PrimaryAllocationByBatchCommand>
    {
        readonly ILogger<PrimaryAllocationByBatchCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public PrimaryAllocationByBatchCommandHandler(ILogger<PrimaryAllocationByBatchCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(PrimaryAllocationByBatchCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing PrimaryAllocationByBatchCommandHandler: {nameof(PrimaryAllocationByBatchCommandHandler)}");

            await this.ProcessHandlerSequence<PrimaryAllocationByBatchPostBusDataPacket, PrimaryAllocationByBatchPostBusSequence, 
                PrimaryAllocationByBatchCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
