using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    /// 
    /// </summary>
    public class VerifyAddNumberOTPCommandHandler : NsbCommandHandler<VerifyAddNumberOTPCommand>
    {
        readonly ILogger<VerifyAddNumberOTPCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public VerifyAddNumberOTPCommandHandler(ILogger<VerifyAddNumberOTPCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(VerifyAddNumberOTPCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing VerifyAddNumberOTPCommandHandler: {nameof(VerifyAddNumberOTPCommandHandler)}");

            await this.ProcessHandlerSequence<VerifyAddNumberOTPPostBusDataPacket, VerifyAddNumberOTPPostBusSequence, 
                VerifyAddNumberOTPCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
