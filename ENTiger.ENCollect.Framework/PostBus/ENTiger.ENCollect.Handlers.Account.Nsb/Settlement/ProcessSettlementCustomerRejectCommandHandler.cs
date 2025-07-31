using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public class ProcessSettlementCustomerRejectCommandHandler : NsbCommandHandler<ProcessSettlementCustomerRejectCommand>
    {
        readonly ILogger<ProcessSettlementCustomerRejectCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ProcessSettlementCustomerRejectCommandHandler(ILogger<ProcessSettlementCustomerRejectCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(ProcessSettlementCustomerRejectCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing ProcessSettlementCustomerRejectCommandHandler: {nameof(ProcessSettlementCustomerRejectCommandHandler)}");

            await this.ProcessHandlerSequence<ProcessSettlementCustomerRejectPostBusDataPacket, ProcessSettlementCustomerRejectPostBusSequence,
                ProcessSettlementCustomerRejectCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
