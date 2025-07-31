using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class MobileForgotPasswordCommandHandler : NsbCommandHandler<MobileForgotPasswordCommand>
    {
        readonly ILogger<MobileForgotPasswordCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public MobileForgotPasswordCommandHandler(ILogger<MobileForgotPasswordCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(MobileForgotPasswordCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing MobileForgotPasswordCommandHandler: {nameof(MobileForgotPasswordCommandHandler)}");

            await this.ProcessHandlerSequence<MobileForgotPasswordPostBusDataPacket, MobileForgotPasswordPostBusSequence, 
                MobileForgotPasswordCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
