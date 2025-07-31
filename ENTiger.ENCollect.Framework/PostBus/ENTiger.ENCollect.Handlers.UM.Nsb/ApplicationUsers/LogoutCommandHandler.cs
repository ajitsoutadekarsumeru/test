using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class LogoutCommandHandler : NsbCommandHandler<LogoutCommand>
    {
        readonly ILogger<LogoutCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public LogoutCommandHandler(ILogger<LogoutCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(LogoutCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing LogoutCommandHandler: {nameof(LogoutCommandHandler)}");

            await this.ProcessHandlerSequence<LogoutPostBusDataPacket, LogoutPostBusSequence, 
                LogoutCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
