using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class MarkEligibleForSettlementCommandHandler : NsbCommandHandler<MarkEligibleForSettlementCommand>
    {
        readonly ILogger<MarkEligibleForSettlementCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public MarkEligibleForSettlementCommandHandler(ILogger<MarkEligibleForSettlementCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(MarkEligibleForSettlementCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing MarkEligibleForSettlementCommandHandler: {nameof(MarkEligibleForSettlementCommandHandler)}");

            await this.ProcessHandlerSequence<MarkEligibleForSettlementPostBusDataPacket, MarkEligibleForSettlementPostBusSequence, 
                MarkEligibleForSettlementCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
