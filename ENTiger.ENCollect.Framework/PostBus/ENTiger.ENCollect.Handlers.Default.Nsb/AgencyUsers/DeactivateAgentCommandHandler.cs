using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class DeactivateAgentCommandHandler : NsbCommandHandler<DeactivateAgentCommand>
    {
        readonly ILogger<DeactivateAgentCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public DeactivateAgentCommandHandler(ILogger<DeactivateAgentCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(DeactivateAgentCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing DeactivateAgentCommandHandler: {nameof(DeactivateAgentCommandHandler)}");

            await this.ProcessHandlerSequence<DeactivateAgentPostBusDataPacket, DeactivateAgentPostBusSequence, 
                DeactivateAgentCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
