using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class GetUnAllocationFileCommandHandler : NsbCommandHandler<GetUnAllocationFileCommand>
    {
        readonly ILogger<GetUnAllocationFileCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public GetUnAllocationFileCommandHandler(ILogger<GetUnAllocationFileCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(GetUnAllocationFileCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing GetUnAllocationFileCommandHandler: {nameof(GetUnAllocationFileCommandHandler)}");

            await this.ProcessHandlerSequence<GetUnAllocationFilePostBusDataPacket, GetUnAllocationFilePostBusSequence, 
                GetUnAllocationFileCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
