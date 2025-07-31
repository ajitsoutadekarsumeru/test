using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class ApproveCollectionCancellationCommandHandler : NsbCommandHandler<ApproveCollectionCancellationCommand>
    {
        readonly ILogger<ApproveCollectionCancellationCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ApproveCollectionCancellationCommandHandler(ILogger<ApproveCollectionCancellationCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(ApproveCollectionCancellationCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing ApproveCollectionCancellationCommandHandler: {nameof(ApproveCollectionCancellationCommandHandler)}");

            await this.ProcessHandlerSequence<ApproveCollectionCancellationPostBusDataPacket, ApproveCollectionCancellationPostBusSequence, 
                ApproveCollectionCancellationCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
