using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllocationFileCommandHandler : NsbCommandHandler<GetAllocationFileCommand>
    {
        readonly ILogger<GetAllocationFileCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public GetAllocationFileCommandHandler(ILogger<GetAllocationFileCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(GetAllocationFileCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing GetAllocationFileCommandHandler: {nameof(GetAllocationFileCommandHandler)}");

            await this.ProcessHandlerSequence<GetAllocationFilePostBusDataPacket, GetAllocationFilePostBusSequence, 
                GetAllocationFileCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
