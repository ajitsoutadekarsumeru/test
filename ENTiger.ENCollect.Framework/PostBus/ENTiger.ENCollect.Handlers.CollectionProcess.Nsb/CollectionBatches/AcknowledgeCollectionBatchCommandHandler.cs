using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.CollectionBatchesModule
{
    /// <summary>
    /// 
    /// </summary>
    public class AcknowledgeCollectionBatchCommandHandler : NsbCommandHandler<AcknowledgeCollectionBatchCommand>
    {
        readonly ILogger<AcknowledgeCollectionBatchCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public AcknowledgeCollectionBatchCommandHandler(ILogger<AcknowledgeCollectionBatchCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(AcknowledgeCollectionBatchCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing AcknowledgeCollectionBatchCommandHandler: {nameof(AcknowledgeCollectionBatchCommandHandler)}");

            await this.ProcessHandlerSequence<AcknowledgeCollectionBatchPostBusDataPacket, AcknowledgeCollectionBatchPostBusSequence, 
                AcknowledgeCollectionBatchCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
