using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public class ProcessSettlementRecommendationCommandHandler : NsbCommandHandler<ProcessSettlementRecommendationCommand>
    {
        readonly ILogger<ProcessSettlementRecommendationCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ProcessSettlementRecommendationCommandHandler(ILogger<ProcessSettlementRecommendationCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(ProcessSettlementRecommendationCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing ProcessSettlementRecommendationCommandHandler: {nameof(ProcessSettlementRecommendationCommandHandler)}");

            await this.ProcessHandlerSequence<ProcessSettlementRecommendationPostBusDataPacket, ProcessSettlementRecommendationPostBusSequence,
                ProcessSettlementRecommendationCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
