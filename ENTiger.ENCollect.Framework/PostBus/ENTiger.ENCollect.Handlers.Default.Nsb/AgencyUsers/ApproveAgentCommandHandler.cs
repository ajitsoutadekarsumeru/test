using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class ApproveAgentCommandHandler : NsbCommandHandler<ApproveAgentCommand>
    {
        readonly ILogger<ApproveAgentCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ApproveAgentCommandHandler(ILogger<ApproveAgentCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(ApproveAgentCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing ApproveAgentCommandHandler: {nameof(ApproveAgentCommandHandler)}");

            await this.ProcessHandlerSequence<ApproveAgentPostBusDataPacket, ApproveAgentPostBusSequence, 
                ApproveAgentCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
