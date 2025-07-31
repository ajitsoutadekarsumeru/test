using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    /// 
    /// </summary>
    public class DisableTreatmentsCommandHandler : NsbCommandHandler<DisableTreatmentsCommand>
    {
        readonly ILogger<DisableTreatmentsCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public DisableTreatmentsCommandHandler(ILogger<DisableTreatmentsCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(DisableTreatmentsCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing DisableTreatmentsCommandHandler: {nameof(DisableTreatmentsCommandHandler)}");

            await this.ProcessHandlerSequence<DisableTreatmentsPostBusDataPacket, DisableTreatmentsPostBusSequence, 
                DisableTreatmentsCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
