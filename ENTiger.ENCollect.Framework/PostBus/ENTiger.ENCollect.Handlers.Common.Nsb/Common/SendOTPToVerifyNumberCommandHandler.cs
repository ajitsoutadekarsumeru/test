using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendOTPToVerifyNumberCommandHandler : NsbCommandHandler<SendOTPToVerifyNumberCommand>
    {
        readonly ILogger<SendOTPToVerifyNumberCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendOTPToVerifyNumberCommandHandler(ILogger<SendOTPToVerifyNumberCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(SendOTPToVerifyNumberCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing SendOTPToVerifyNumberCommandHandler: {nameof(SendOTPToVerifyNumberCommandHandler)}");

            await this.ProcessHandlerSequence<SendOTPToVerifyNumberPostBusDataPacket, SendOTPToVerifyNumberPostBusSequence, 
                SendOTPToVerifyNumberCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
