using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendAccountMessageCommandHandler : NsbCommandHandler<SendAccountMessageCommand>
    {
        readonly ILogger<SendAccountMessageCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendAccountMessageCommandHandler(ILogger<SendAccountMessageCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(SendAccountMessageCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing SendAccountMessageCommandHandler: {nameof(SendAccountMessageCommandHandler)}");

            await this.ProcessHandlerSequence<SendAccountMessagePostBusDataPacket, SendAccountMessagePostBusSequence, 
                SendAccountMessageCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
