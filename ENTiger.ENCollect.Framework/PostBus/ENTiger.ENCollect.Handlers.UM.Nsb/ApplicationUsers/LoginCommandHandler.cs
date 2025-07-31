using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginCommandHandler : NsbCommandHandler<LoginCommand>
    {
        readonly ILogger<LoginCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public LoginCommandHandler(ILogger<LoginCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(LoginCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing LoginCommandHandler: {nameof(LoginCommandHandler)}");

            await this.ProcessHandlerSequence<LoginPostBusDataPacket, LoginPostBusSequence, 
                LoginCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
