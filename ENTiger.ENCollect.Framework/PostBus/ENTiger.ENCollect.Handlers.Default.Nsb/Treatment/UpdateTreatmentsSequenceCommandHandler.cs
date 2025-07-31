using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateTreatmentsSequenceCommandHandler : NsbCommandHandler<UpdateTreatmentsSequenceCommand>
    {
        readonly ILogger<UpdateTreatmentsSequenceCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateTreatmentsSequenceCommandHandler(ILogger<UpdateTreatmentsSequenceCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(UpdateTreatmentsSequenceCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing UpdateTreatmentsSequenceCommandHandler: {nameof(UpdateTreatmentsSequenceCommandHandler)}");

            await this.ProcessHandlerSequence<UpdateTreatmentsSequencePostBusDataPacket, UpdateTreatmentsSequencePostBusSequence, 
                UpdateTreatmentsSequenceCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
