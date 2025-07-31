using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public class ProcessSettlementNegotiateCommandHandler : NsbCommandHandler<ProcessSettlementNegotiateCommand>
    {
        readonly ILogger<ProcessSettlementNegotiateCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ProcessSettlementNegotiateCommandHandler(ILogger<ProcessSettlementNegotiateCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(ProcessSettlementNegotiateCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing ProcessSettlementNegotiateCommandHandler: {nameof(ProcessSettlementNegotiateCommandHandler)}");

            await this.ProcessHandlerSequence<ProcessSettlementNegotiatePostBusDataPacket, ProcessSettlementNegotiatePostBusSequence,
                ProcessSettlementNegotiateCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
