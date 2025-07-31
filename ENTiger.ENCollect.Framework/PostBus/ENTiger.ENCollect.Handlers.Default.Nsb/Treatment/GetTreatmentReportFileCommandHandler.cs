using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    /// 
    /// </summary>
    public class GetTreatmentReportFileCommandHandler : NsbCommandHandler<GetTreatmentReportFileCommand>
    {
        readonly ILogger<GetTreatmentReportFileCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public GetTreatmentReportFileCommandHandler(ILogger<GetTreatmentReportFileCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(GetTreatmentReportFileCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing GetTreatmentReportFileCommandHandler: {nameof(GetTreatmentReportFileCommandHandler)}");

            await this.ProcessHandlerSequence<GetTreatmentReportFilePostBusDataPacket, GetTreatmentReportFilePostBusSequence, 
                GetTreatmentReportFileCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
