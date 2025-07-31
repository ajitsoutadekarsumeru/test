using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendPaymentLinkCommandHandler : NsbCommandHandler<SendPaymentLinkCommand>
    {
        readonly ILogger<SendPaymentLinkCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendPaymentLinkCommandHandler(ILogger<SendPaymentLinkCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(SendPaymentLinkCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing SendPaymentLinkCommandHandler: {nameof(SendPaymentLinkCommandHandler)}");

            await this.ProcessHandlerSequence<SendPaymentLinkPostBusDataPacket, SendPaymentLinkPostBusSequence, 
                SendPaymentLinkCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
