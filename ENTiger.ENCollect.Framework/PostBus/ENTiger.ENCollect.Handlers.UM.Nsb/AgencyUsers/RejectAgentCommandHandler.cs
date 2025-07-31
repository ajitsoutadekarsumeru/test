using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class RejectAgentCommandHandler : NsbCommandHandler<RejectAgentCommand>
    {
        readonly ILogger<RejectAgentCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public RejectAgentCommandHandler(ILogger<RejectAgentCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(RejectAgentCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing RejectAgentCommandHandler: {nameof(RejectAgentCommandHandler)}");

            await this.ProcessHandlerSequence<RejectAgentPostBusDataPacket, RejectAgentPostBusSequence, 
                RejectAgentCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
