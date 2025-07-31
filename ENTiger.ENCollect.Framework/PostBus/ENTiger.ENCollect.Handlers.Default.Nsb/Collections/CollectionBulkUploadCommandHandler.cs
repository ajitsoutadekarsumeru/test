using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class CollectionBulkUploadCommandHandler : NsbCommandHandler<CollectionBulkUploadCommand>
    {
        readonly ILogger<CollectionBulkUploadCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public CollectionBulkUploadCommandHandler(ILogger<CollectionBulkUploadCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(CollectionBulkUploadCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing CollectionBulkUploadCommandHandler: {nameof(CollectionBulkUploadCommandHandler)}");

            await this.ProcessHandlerSequence<CollectionBulkUploadPostBusDataPacket, CollectionBulkUploadPostBusSequence, 
                CollectionBulkUploadCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
