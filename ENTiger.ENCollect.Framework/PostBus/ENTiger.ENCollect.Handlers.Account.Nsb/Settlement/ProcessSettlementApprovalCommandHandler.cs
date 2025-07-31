using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public class ProcessSettlementApprovalCommandHandler : NsbCommandHandler<ProcessSettlementApprovalCommand>
    {
        readonly ILogger<ProcessSettlementApprovalCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ProcessSettlementApprovalCommandHandler(ILogger<ProcessSettlementApprovalCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(ProcessSettlementApprovalCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing ProcessSettlementApprovalCommandHandler: {nameof(ProcessSettlementApprovalCommandHandler)}");

            await this.ProcessHandlerSequence<ProcessSettlementApprovalPostBusDataPacket, ProcessSettlementApprovalPostBusSequence,
                ProcessSettlementApprovalCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
