using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class ValidateAgentCommandHandler : NsbCommandHandler<ValidateAgentCommand>
    {
        readonly ILogger<ValidateAgentCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ValidateAgentCommandHandler(ILogger<ValidateAgentCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(ValidateAgentCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing ValidateAgentCommandHandler: {nameof(ValidateAgentCommandHandler)}");

            await this.ProcessHandlerSequence<ValidateAgentPostBusDataPacket, ValidateAgentPostBusSequence, 
                ValidateAgentCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
