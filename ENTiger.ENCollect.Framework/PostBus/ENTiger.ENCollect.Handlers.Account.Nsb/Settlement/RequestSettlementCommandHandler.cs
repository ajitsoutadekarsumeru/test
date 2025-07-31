using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public class RequestSettlementCommandHandler : NsbCommandHandler<RequestSettlementCommand>
    {
        readonly ILogger<RequestSettlementCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public RequestSettlementCommandHandler(ILogger<RequestSettlementCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(RequestSettlementCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing RequestSettlementCommandHandler: {nameof(RequestSettlementCommandHandler)}");

            await this.ProcessHandlerSequence<RequestSettlementPostBusDataPacket, RequestSettlementPostBusSequence, 
                RequestSettlementCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
