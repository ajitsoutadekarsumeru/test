using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class GetFileCommandHandler : NsbCommandHandler<GetFileCommand>
    {
        readonly ILogger<GetFileCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public GetFileCommandHandler(ILogger<GetFileCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(GetFileCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing GetFileCommandHandler: {nameof(GetFileCommandHandler)}");

            await this.ProcessHandlerSequence<GetFilePostBusDataPacket, GetFilePostBusSequence, 
                GetFileCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
