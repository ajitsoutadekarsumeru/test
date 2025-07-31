using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class GetCollectionImageCommandHandler : NsbCommandHandler<GetCollectionImageCommand>
    {
        readonly ILogger<GetCollectionImageCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public GetCollectionImageCommandHandler(ILogger<GetCollectionImageCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(GetCollectionImageCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing GetCollectionImageCommandHandler: {nameof(GetCollectionImageCommandHandler)}");

            await this.ProcessHandlerSequence<GetCollectionImagePostBusDataPacket, GetCollectionImagePostBusSequence, 
                GetCollectionImageCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
