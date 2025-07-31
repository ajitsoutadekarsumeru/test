using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.CollectionBatchesModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateCollectionBatchCommandHandler : NsbCommandHandler<UpdateCollectionBatchCommand>
    {
        readonly ILogger<UpdateCollectionBatchCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateCollectionBatchCommandHandler(ILogger<UpdateCollectionBatchCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(UpdateCollectionBatchCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing UpdateCollectionBatchCommandHandler: {nameof(UpdateCollectionBatchCommandHandler)}");

            await this.ProcessHandlerSequence<UpdateCollectionBatchPostBusDataPacket, UpdateCollectionBatchPostBusSequence, 
                UpdateCollectionBatchCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
