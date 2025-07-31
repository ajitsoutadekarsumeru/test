using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class ResetPasswordCommandHandler : NsbCommandHandler<ResetPasswordCommand>
    {
        readonly ILogger<ResetPasswordCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ResetPasswordCommandHandler(ILogger<ResetPasswordCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(ResetPasswordCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing ResetPasswordCommandHandler: {nameof(ResetPasswordCommandHandler)}");

            await this.ProcessHandlerSequence<ResetPasswordPostBusDataPacket, ResetPasswordPostBusSequence, 
                ResetPasswordCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
