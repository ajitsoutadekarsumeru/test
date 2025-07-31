using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAgentImageCommandHandler : NsbCommandHandler<GetAgentImageCommand>
    {
        readonly ILogger<GetAgentImageCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public GetAgentImageCommandHandler(ILogger<GetAgentImageCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(GetAgentImageCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing GetAgentImageCommandHandler: {nameof(GetAgentImageCommandHandler)}");

            await this.ProcessHandlerSequence<GetAgentImagePostBusDataPacket, GetAgentImagePostBusSequence, 
                GetAgentImageCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
