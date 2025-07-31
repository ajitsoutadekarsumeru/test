using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class AddAgentCommandHandler : NsbCommandHandler<AddAgentCommand>
    {
        readonly ILogger<AddAgentCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public AddAgentCommandHandler(ILogger<AddAgentCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(AddAgentCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing AddAgentCommandHandler: {nameof(AddAgentCommandHandler)}");

            await this.ProcessHandlerSequence<AddAgentPostBusDataPacket, AddAgentPostBusSequence, 
                AddAgentCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
