using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendVideoCallLinkCommandHandler : NsbCommandHandler<SendVideoCallLinkCommand>
    {
        readonly ILogger<SendVideoCallLinkCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendVideoCallLinkCommandHandler(ILogger<SendVideoCallLinkCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(SendVideoCallLinkCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing SendVideoCallLinkCommandHandler: {nameof(SendVideoCallLinkCommandHandler)}");

            await this.ProcessHandlerSequence<SendVideoCallLinkPostBusDataPacket, SendVideoCallLinkPostBusSequence, 
                SendVideoCallLinkCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
