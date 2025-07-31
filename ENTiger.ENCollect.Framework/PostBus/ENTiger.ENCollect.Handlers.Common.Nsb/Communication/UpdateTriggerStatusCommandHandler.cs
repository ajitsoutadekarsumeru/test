using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateTriggerStatusCommandHandler : NsbCommandHandler<UpdateTriggerStatusCommand>
    {
        readonly ILogger<UpdateTriggerStatusCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateTriggerStatusCommandHandler(ILogger<UpdateTriggerStatusCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(UpdateTriggerStatusCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing UpdateTriggerStatusCommandHandler: {nameof(UpdateTriggerStatusCommandHandler)}");

            await this.ProcessHandlerSequence<UpdateTriggerStatusPostBusDataPacket, UpdateTriggerStatusPostBusSequence, 
                UpdateTriggerStatusCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
