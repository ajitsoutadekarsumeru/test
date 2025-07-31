using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateAgentCommandHandler : NsbCommandHandler<UpdateAgentCommand>
    {
        readonly ILogger<UpdateAgentCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateAgentCommandHandler(ILogger<UpdateAgentCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(UpdateAgentCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing UpdateAgentCommandHandler: {nameof(UpdateAgentCommandHandler)}");

            await this.ProcessHandlerSequence<UpdateAgentPostBusDataPacket, UpdateAgentPostBusSequence, 
                UpdateAgentCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
