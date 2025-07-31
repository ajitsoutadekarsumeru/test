using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class ForgotPasswordCommandHandler : NsbCommandHandler<ForgotPasswordCommand>
    {
        readonly ILogger<ForgotPasswordCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ForgotPasswordCommandHandler(ILogger<ForgotPasswordCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(ForgotPasswordCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing ForgotPasswordCommandHandler: {nameof(ForgotPasswordCommandHandler)}");

            await this.ProcessHandlerSequence<ForgotPasswordPostBusDataPacket, ForgotPasswordPostBusSequence, 
                ForgotPasswordCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
