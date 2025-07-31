using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class RejectCollectionCancellationCommandHandler : NsbCommandHandler<RejectCollectionCancellationCommand>
    {
        readonly ILogger<RejectCollectionCancellationCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public RejectCollectionCancellationCommandHandler(ILogger<RejectCollectionCancellationCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(RejectCollectionCancellationCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing RejectCollectionCancellationCommandHandler: {nameof(RejectCollectionCancellationCommandHandler)}");

            await this.ProcessHandlerSequence<RejectCollectionCancellationPostBusDataPacket, RejectCollectionCancellationPostBusSequence, 
                RejectCollectionCancellationCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
