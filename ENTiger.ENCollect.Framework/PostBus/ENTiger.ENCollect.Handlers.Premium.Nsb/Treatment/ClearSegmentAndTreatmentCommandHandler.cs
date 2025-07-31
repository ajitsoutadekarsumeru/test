using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    /// 
    /// </summary>
    public class ClearSegmentAndTreatmentCommandHandler : NsbCommandHandler<ClearSegmentAndTreatmentCommand>
    {
        readonly ILogger<ClearSegmentAndTreatmentCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ClearSegmentAndTreatmentCommandHandler(ILogger<ClearSegmentAndTreatmentCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(ClearSegmentAndTreatmentCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing ClearSegmentAndTreatmentCommandHandler: {nameof(ClearSegmentAndTreatmentCommandHandler)}");

            await this.ProcessHandlerSequence<ClearSegmentAndTreatmentPostBusDataPacket, ClearSegmentAndTreatmentPostBusSequence, 
                ClearSegmentAndTreatmentCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
