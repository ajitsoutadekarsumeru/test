using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendPaymentCopyViaEmailCommandHandler : NsbCommandHandler<SendPaymentCopyViaEmailCommand>
    {
        readonly ILogger<SendPaymentCopyViaEmailCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendPaymentCopyViaEmailCommandHandler(ILogger<SendPaymentCopyViaEmailCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(SendPaymentCopyViaEmailCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing SendPaymentCopyViaEmailCommandHandler: {nameof(SendPaymentCopyViaEmailCommandHandler)}");

            await this.ProcessHandlerSequence<SendPaymentCopyViaEmailPostBusDataPacket, SendPaymentCopyViaEmailPostBusSequence, 
                SendPaymentCopyViaEmailCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
