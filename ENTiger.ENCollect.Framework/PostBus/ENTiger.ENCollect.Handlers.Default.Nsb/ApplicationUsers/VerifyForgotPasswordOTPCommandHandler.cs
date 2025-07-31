using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class VerifyForgotPasswordOTPCommandHandler : NsbCommandHandler<VerifyForgotPasswordOTPCommand>
    {
        readonly ILogger<VerifyForgotPasswordOTPCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public VerifyForgotPasswordOTPCommandHandler(ILogger<VerifyForgotPasswordOTPCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(VerifyForgotPasswordOTPCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing VerifyForgotPasswordOTPCommandHandler: {nameof(VerifyForgotPasswordOTPCommandHandler)}");

            await this.ProcessHandlerSequence<VerifyForgotPasswordOTPPostBusDataPacket, VerifyForgotPasswordOTPPostBusSequence, 
                VerifyForgotPasswordOTPCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
