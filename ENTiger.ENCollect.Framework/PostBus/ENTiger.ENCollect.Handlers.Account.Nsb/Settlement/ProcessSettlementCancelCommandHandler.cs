using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public class ProcessSettlementCancelCommandHandler : NsbCommandHandler<CancelSettlementCommand>
    {
        readonly ILogger<ProcessSettlementCancelCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ProcessSettlementCancelCommandHandler(ILogger<ProcessSettlementCancelCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(CancelSettlementCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing ProcessSettlementCancelCommandHandler: {nameof(ProcessSettlementCancelCommandHandler)}");

            await this.ProcessHandlerSequence<CancelSettlementPostBusDataPacket, CancelSettlementPostBusSequence,
                CancelSettlementCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
