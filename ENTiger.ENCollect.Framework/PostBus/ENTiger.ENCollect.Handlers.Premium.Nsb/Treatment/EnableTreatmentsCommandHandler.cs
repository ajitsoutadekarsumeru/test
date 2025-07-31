using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    /// 
    /// </summary>
    public class EnableTreatmentsCommandHandler : NsbCommandHandler<EnableTreatmentsCommand>
    {
        readonly ILogger<EnableTreatmentsCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public EnableTreatmentsCommandHandler(ILogger<EnableTreatmentsCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(EnableTreatmentsCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing EnableTreatmentsCommandHandler: {nameof(EnableTreatmentsCommandHandler)}");

            await this.ProcessHandlerSequence<EnableTreatmentsPostBusDataPacket, EnableTreatmentsPostBusSequence, 
                EnableTreatmentsCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
