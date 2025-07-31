using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteTreatmentsCommandHandler : NsbCommandHandler<DeleteTreatmentsCommand>
    {
        readonly ILogger<DeleteTreatmentsCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public DeleteTreatmentsCommandHandler(ILogger<DeleteTreatmentsCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(DeleteTreatmentsCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing DeleteTreatmentsCommandHandler: {nameof(DeleteTreatmentsCommandHandler)}");

            await this.ProcessHandlerSequence<DeleteTreatmentsPostBusDataPacket, DeleteTreatmentsPostBusSequence, 
                DeleteTreatmentsCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
