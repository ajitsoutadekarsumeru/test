using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendPaymentCopyViaSMSCommandHandler : NsbCommandHandler<SendPaymentCopyViaSMSCommand>
    {
        readonly ILogger<SendPaymentCopyViaSMSCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendPaymentCopyViaSMSCommandHandler(ILogger<SendPaymentCopyViaSMSCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(SendPaymentCopyViaSMSCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing SendPaymentCopyViaSMSCommandHandler: {nameof(SendPaymentCopyViaSMSCommandHandler)}");

            await this.ProcessHandlerSequence<SendPaymentCopyViaSMSPostBusDataPacket, SendPaymentCopyViaSMSPostBusSequence, 
                SendPaymentCopyViaSMSCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
