using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SecondaryUnAllocationByBatchCommandHandler : NsbCommandHandler<SecondaryUnAllocationByBatchCommand>
    {
        readonly ILogger<SecondaryUnAllocationByBatchCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SecondaryUnAllocationByBatchCommandHandler(ILogger<SecondaryUnAllocationByBatchCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(SecondaryUnAllocationByBatchCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing SecondaryUnAllocationByBatchCommandHandler: {nameof(SecondaryUnAllocationByBatchCommandHandler)}");

            await this.ProcessHandlerSequence<SecondaryUnAllocationByBatchPostBusDataPacket, SecondaryUnAllocationByBatchPostBusSequence, 
                SecondaryUnAllocationByBatchCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
