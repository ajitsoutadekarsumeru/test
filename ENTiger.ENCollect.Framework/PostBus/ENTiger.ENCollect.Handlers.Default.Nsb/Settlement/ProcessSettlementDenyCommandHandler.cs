using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public class ProcessSettlementDenyCommandHandler : NsbCommandHandler<ProcessSettlementDenyCommand>
    {
        readonly ILogger<ProcessSettlementDenyCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ProcessSettlementDenyCommandHandler(ILogger<ProcessSettlementDenyCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(ProcessSettlementDenyCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing ProcessSettlementDenyCommandHandler: {nameof(ProcessSettlementDenyCommandHandler)}");

            await this.ProcessHandlerSequence<ProcessSettlementDenyPostBusDataPacket, ProcessSettlementDenyPostBusSequence,
                ProcessSettlementDenyCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
