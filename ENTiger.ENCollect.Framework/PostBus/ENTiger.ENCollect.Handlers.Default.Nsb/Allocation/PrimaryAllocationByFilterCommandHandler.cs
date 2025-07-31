using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class PrimaryAllocationByFilterCommandHandler : NsbCommandHandler<PrimaryAllocationByFilterCommand>
    {
        readonly ILogger<PrimaryAllocationByFilterCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public PrimaryAllocationByFilterCommandHandler(ILogger<PrimaryAllocationByFilterCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(PrimaryAllocationByFilterCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing PrimaryAllocationByFilterCommandHandler: {nameof(PrimaryAllocationByFilterCommandHandler)}");

            await this.ProcessHandlerSequence<PrimaryAllocationByFilterPostBusDataPacket, PrimaryAllocationByFilterPostBusSequence, 
                PrimaryAllocationByFilterCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
