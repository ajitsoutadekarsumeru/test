using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class RenewAgentCommandHandler : NsbCommandHandler<RenewAgentCommand>
    {
        readonly ILogger<RenewAgentCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public RenewAgentCommandHandler(ILogger<RenewAgentCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(RenewAgentCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing RenewAgentCommandHandler: {nameof(RenewAgentCommandHandler)}");

            await this.ProcessHandlerSequence<RenewAgentPostBusDataPacket, RenewAgentPostBusSequence, 
                RenewAgentCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
