using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    /// 
    /// </summary>
    public class GetImageCommandHandler : NsbCommandHandler<GetImageCommand>
    {
        readonly ILogger<GetImageCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public GetImageCommandHandler(ILogger<GetImageCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(GetImageCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing GetImageCommandHandler: {nameof(GetImageCommandHandler)}");

            await this.ProcessHandlerSequence<GetImagePostBusDataPacket, GetImagePostBusSequence, 
                GetImageCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
