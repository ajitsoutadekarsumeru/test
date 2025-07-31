using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteSegmentsCommandHandler : NsbCommandHandler<DeleteSegmentsCommand>
    {
        readonly ILogger<DeleteSegmentsCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public DeleteSegmentsCommandHandler(ILogger<DeleteSegmentsCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(DeleteSegmentsCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing DeleteSegmentsCommandHandler: {nameof(DeleteSegmentsCommandHandler)}");

            await this.ProcessHandlerSequence<DeleteSegmentsPostBusDataPacket, DeleteSegmentsPostBusSequence, 
                DeleteSegmentsCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
