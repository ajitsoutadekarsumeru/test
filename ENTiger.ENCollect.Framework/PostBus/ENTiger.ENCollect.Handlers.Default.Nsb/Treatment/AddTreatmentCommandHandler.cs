using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    /// 
    /// </summary>
    public class AddTreatmentCommandHandler : NsbCommandHandler<AddTreatmentCommand>
    {
        readonly ILogger<AddTreatmentCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public AddTreatmentCommandHandler(ILogger<AddTreatmentCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(AddTreatmentCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing AddTreatmentCommandHandler: {nameof(AddTreatmentCommandHandler)}");

            await this.ProcessHandlerSequence<AddTreatmentPostBusDataPacket, AddTreatmentPostBusSequence, 
                AddTreatmentCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
