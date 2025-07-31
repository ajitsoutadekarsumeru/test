using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.DesignationsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class AssignSchemeCommandHandler : NsbCommandHandler<AssignSchemeCommand>
    {
        readonly ILogger<AssignSchemeCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public AssignSchemeCommandHandler(ILogger<AssignSchemeCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(AssignSchemeCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing AssignSchemeCommandHandler: {nameof(AssignSchemeCommandHandler)}");

            await this.ProcessHandlerSequence<AssignSchemePostBusDataPacket, AssignSchemePostBusSequence, 
                AssignSchemeCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
