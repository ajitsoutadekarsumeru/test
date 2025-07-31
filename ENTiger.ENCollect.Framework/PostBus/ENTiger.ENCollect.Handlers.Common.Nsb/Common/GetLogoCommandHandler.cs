using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    /// 
    /// </summary>
    public class GetLogoCommandHandler : NsbCommandHandler<GetLogoCommand>
    {
        readonly ILogger<GetLogoCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public GetLogoCommandHandler(ILogger<GetLogoCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(GetLogoCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing GetLogoCommandHandler: {nameof(GetLogoCommandHandler)}");

            await this.ProcessHandlerSequence<GetLogoPostBusDataPacket, GetLogoPostBusSequence, 
                GetLogoCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
