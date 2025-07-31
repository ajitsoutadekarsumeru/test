using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class RunTriggersCommandHandler : NsbCommandHandler<RunTriggersCommand>
    {
        readonly ILogger<RunTriggersCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public RunTriggersCommandHandler(ILogger<RunTriggersCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(RunTriggersCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing RunTriggersCommandHandler: {nameof(RunTriggersCommandHandler)}");

            await this.ProcessHandlerSequence<RunTriggersPostBusDataPacket, RunTriggersPostBusSequence, 
                RunTriggersCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
