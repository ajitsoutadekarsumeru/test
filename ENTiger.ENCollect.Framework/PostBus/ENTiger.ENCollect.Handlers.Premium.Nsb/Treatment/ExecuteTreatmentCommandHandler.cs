using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    /// 
    /// </summary>
    public class ExecuteTreatmentCommandHandler : NsbCommandHandler<ExecuteTreatmentCommand>
    {
        readonly ILogger<ExecuteTreatmentCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ExecuteTreatmentCommandHandler(ILogger<ExecuteTreatmentCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(ExecuteTreatmentCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing ExecuteTreatmentCommandHandler: {nameof(ExecuteTreatmentCommandHandler)}");

            await this.ProcessHandlerSequence<ExecuteTreatmentPostBusDataPacket, ExecuteTreatmentPostBusSequence, 
                ExecuteTreatmentCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
