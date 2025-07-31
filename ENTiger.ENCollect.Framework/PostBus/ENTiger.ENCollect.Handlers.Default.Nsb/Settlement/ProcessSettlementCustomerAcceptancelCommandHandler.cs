using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public class ProcessSettlementCustomerAcceptancelCommandHandler : NsbCommandHandler<ProcessSettlementCustomerAcceptanceCommand>
    {
        readonly ILogger<ProcessSettlementCustomerAcceptancelCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ProcessSettlementCustomerAcceptancelCommandHandler(ILogger<ProcessSettlementCustomerAcceptancelCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(ProcessSettlementCustomerAcceptanceCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing ProcessSettlementCustomerAcceptancelCommandHandler: {nameof(ProcessSettlementCustomerAcceptancelCommandHandler)}");

            await this.ProcessHandlerSequence<ProcessSettlementCustomerAcceptancePostBusDataPacket, ProcessSettlementCustomerAcceptancePostBusSequence,
                ProcessSettlementCustomerAcceptanceCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
