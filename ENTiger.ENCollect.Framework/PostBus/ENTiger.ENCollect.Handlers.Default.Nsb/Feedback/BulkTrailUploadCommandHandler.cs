using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.FeedbackModule
{
    /// <summary>
    /// 
    /// </summary>
    public class BulkTrailUploadCommandHandler : NsbCommandHandler<BulkTrailUploadCommand>
    {
        readonly ILogger<BulkTrailUploadCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public BulkTrailUploadCommandHandler(ILogger<BulkTrailUploadCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(BulkTrailUploadCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing BulkTrailUploadCommandHandler: {nameof(BulkTrailUploadCommandHandler)}");

            await this.ProcessHandlerSequence<BulkTrailUploadPostBusDataPacket, BulkTrailUploadPostBusSequence, 
                BulkTrailUploadCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
