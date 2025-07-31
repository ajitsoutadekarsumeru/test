using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class MobileResetPasswordCommandHandler : NsbCommandHandler<MobileResetPasswordCommand>
    {
        readonly ILogger<MobileResetPasswordCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public MobileResetPasswordCommandHandler(ILogger<MobileResetPasswordCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(MobileResetPasswordCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing MobileResetPasswordCommandHandler: {nameof(MobileResetPasswordCommandHandler)}");

            await this.ProcessHandlerSequence<MobileResetPasswordPostBusDataPacket, MobileResetPasswordPostBusSequence, 
                MobileResetPasswordCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
