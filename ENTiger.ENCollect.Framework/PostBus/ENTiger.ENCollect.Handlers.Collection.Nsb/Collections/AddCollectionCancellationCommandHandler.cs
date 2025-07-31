using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class AddCollectionCancellationCommandHandler : NsbCommandHandler<AddCollectionCancellationCommand>
    {
        readonly ILogger<AddCollectionCancellationCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public AddCollectionCancellationCommandHandler(ILogger<AddCollectionCancellationCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(AddCollectionCancellationCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing AddCollectionCancellationCommandHandler: {nameof(AddCollectionCancellationCommandHandler)}");

            await this.ProcessHandlerSequence<AddCollectionCancellationPostBusDataPacket, AddCollectionCancellationPostBusSequence, 
                AddCollectionCancellationCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
