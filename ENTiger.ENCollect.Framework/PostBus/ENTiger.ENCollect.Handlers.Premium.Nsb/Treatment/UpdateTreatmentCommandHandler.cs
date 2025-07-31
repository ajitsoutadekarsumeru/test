using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateTreatmentCommandHandler : NsbCommandHandler<UpdateTreatmentCommand>
    {
        readonly ILogger<UpdateTreatmentCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateTreatmentCommandHandler(ILogger<UpdateTreatmentCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(UpdateTreatmentCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing UpdateTreatmentCommandHandler: {nameof(UpdateTreatmentCommandHandler)}");

            await this.ProcessHandlerSequence<UpdateTreatmentPostBusDataPacket, UpdateTreatmentPostBusSequence, 
                UpdateTreatmentCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
