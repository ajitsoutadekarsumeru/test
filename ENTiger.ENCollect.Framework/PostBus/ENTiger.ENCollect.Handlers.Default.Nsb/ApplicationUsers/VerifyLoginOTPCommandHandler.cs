using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class VerifyLoginOTPCommandHandler : NsbCommandHandler<VerifyLoginOTPCommand>
    {
        readonly ILogger<VerifyLoginOTPCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public VerifyLoginOTPCommandHandler(ILogger<VerifyLoginOTPCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(VerifyLoginOTPCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing VerifyLoginOTPCommandHandler: {nameof(VerifyLoginOTPCommandHandler)}");

            await this.ProcessHandlerSequence<VerifyLoginOTPPostBusDataPacket, VerifyLoginOTPPostBusSequence, 
                VerifyLoginOTPCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
