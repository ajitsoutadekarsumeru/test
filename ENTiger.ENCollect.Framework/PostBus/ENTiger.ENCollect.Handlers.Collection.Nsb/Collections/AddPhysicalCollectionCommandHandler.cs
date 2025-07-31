using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class AddPhysicalCollectionCommandHandler : NsbCommandHandler<AddPhysicalCollectionCommand>
    {
        readonly ILogger<AddPhysicalCollectionCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public AddPhysicalCollectionCommandHandler(ILogger<AddPhysicalCollectionCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(AddPhysicalCollectionCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing AddPhysicalCollectionCommandHandler: {nameof(AddPhysicalCollectionCommandHandler)}");

            await this.ProcessHandlerSequence<AddPhysicalCollectionPostBusDataPacket, AddPhysicalCollectionPostBusSequence, 
                AddPhysicalCollectionCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
