using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class GetCollectionFileCommandHandler : NsbCommandHandler<GetCollectionFileCommand>
    {
        readonly ILogger<GetCollectionFileCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public GetCollectionFileCommandHandler(ILogger<GetCollectionFileCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(GetCollectionFileCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing GetCollectionFileCommandHandler: {nameof(GetCollectionFileCommandHandler)}");

            await this.ProcessHandlerSequence<GetCollectionFilePostBusDataPacket, GetCollectionFilePostBusSequence, 
                GetCollectionFileCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
