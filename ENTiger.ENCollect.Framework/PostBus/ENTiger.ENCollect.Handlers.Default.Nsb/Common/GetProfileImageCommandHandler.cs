using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    /// 
    /// </summary>
    public class GetProfileImageCommandHandler : NsbCommandHandler<GetProfileImageCommand>
    {
        readonly ILogger<GetProfileImageCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public GetProfileImageCommandHandler(ILogger<GetProfileImageCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(GetProfileImageCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing GetProfileImageCommandHandler: {nameof(GetProfileImageCommandHandler)}");

            await this.ProcessHandlerSequence<GetProfileImagePostBusDataPacket, GetProfileImagePostBusSequence, 
                GetProfileImageCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
