using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SecondaryAllocationByFilterCommandHandler : NsbCommandHandler<SecondaryAllocationByFilterCommand>
    {
        readonly ILogger<SecondaryAllocationByFilterCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SecondaryAllocationByFilterCommandHandler(ILogger<SecondaryAllocationByFilterCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(SecondaryAllocationByFilterCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing SecondaryAllocationByFilterCommandHandler: {nameof(SecondaryAllocationByFilterCommandHandler)}");

            await this.ProcessHandlerSequence<SecondaryAllocationByFilterPostBusDataPacket, SecondaryAllocationByFilterPostBusSequence, 
                SecondaryAllocationByFilterCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
