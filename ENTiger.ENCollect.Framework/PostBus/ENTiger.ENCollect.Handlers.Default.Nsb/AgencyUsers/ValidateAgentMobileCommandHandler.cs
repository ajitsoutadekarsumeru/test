using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class ValidateAgentMobileCommandHandler : NsbCommandHandler<ValidateAgentMobileCommand>
    {
        readonly ILogger<ValidateAgentMobileCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ValidateAgentMobileCommandHandler(ILogger<ValidateAgentMobileCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(ValidateAgentMobileCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing ValidateAgentMobileCommandHandler: {nameof(ValidateAgentMobileCommandHandler)}");

            await this.ProcessHandlerSequence<ValidateAgentMobilePostBusDataPacket, ValidateAgentMobilePostBusSequence, 
                ValidateAgentMobileCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
