using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class AddCollectionCommandHandler : NsbCommandHandler<AddCollectionCommand>
    {
        readonly ILogger<AddCollectionCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public AddCollectionCommandHandler(ILogger<AddCollectionCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(AddCollectionCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing AddCollectionCommandHandler: {nameof(AddCollectionCommandHandler)}");

            await this.ProcessHandlerSequence<AddCollectionPostBusDataPacket, AddCollectionPostBusSequence, 
                AddCollectionCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
