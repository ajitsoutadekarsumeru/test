using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class ValidateAgentEmailCommandHandler : NsbCommandHandler<ValidateAgentEmailCommand>
    {
        readonly ILogger<ValidateAgentEmailCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ValidateAgentEmailCommandHandler(ILogger<ValidateAgentEmailCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(ValidateAgentEmailCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing ValidateAgentEmailCommandHandler: {nameof(ValidateAgentEmailCommandHandler)}");

            await this.ProcessHandlerSequence<ValidateAgentEmailPostBusDataPacket, ValidateAgentEmailPostBusSequence, 
                ValidateAgentEmailCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
