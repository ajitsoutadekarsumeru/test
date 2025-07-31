using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class AcknowledgeCollectionsCommandHandler : NsbCommandHandler<AcknowledgeCollectionsCommand>
    {
        readonly ILogger<AcknowledgeCollectionsCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public AcknowledgeCollectionsCommandHandler(ILogger<AcknowledgeCollectionsCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(AcknowledgeCollectionsCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing AcknowledgeCollectionsCommandHandler: {nameof(AcknowledgeCollectionsCommandHandler)}");

            await this.ProcessHandlerSequence<AcknowledgeCollectionsPostBusDataPacket, AcknowledgeCollectionsPostBusSequence, 
                AcknowledgeCollectionsCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
