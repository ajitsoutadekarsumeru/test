using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class GetKeyCommandHandler : NsbCommandHandler<GetKeyCommand>
    {
        readonly ILogger<GetKeyCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public GetKeyCommandHandler(ILogger<GetKeyCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(GetKeyCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing GetKeyCommandHandler: {nameof(GetKeyCommandHandler)}");

            await this.ProcessHandlerSequence<GetKeyPostBusDataPacket, GetKeyPostBusSequence, 
                GetKeyCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
