using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.CollectionBatchesModule
{
    /// <summary>
    /// 
    /// </summary>
    public class AddCollectionBatchCommandHandler : NsbCommandHandler<AddCollectionBatchCommand>
    {
        readonly ILogger<AddCollectionBatchCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public AddCollectionBatchCommandHandler(ILogger<AddCollectionBatchCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(AddCollectionBatchCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing AddCollectionBatchCommandHandler: {nameof(AddCollectionBatchCommandHandler)}");

            await this.ProcessHandlerSequence<AddCollectionBatchPostBusDataPacket, AddCollectionBatchPostBusSequence, 
                AddCollectionBatchCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
