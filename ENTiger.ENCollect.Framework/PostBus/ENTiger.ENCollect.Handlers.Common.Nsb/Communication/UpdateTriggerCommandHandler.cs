using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateTriggerCommandHandler : NsbCommandHandler<UpdateTriggerCommand>
    {
        readonly ILogger<UpdateTriggerCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateTriggerCommandHandler(ILogger<UpdateTriggerCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(UpdateTriggerCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing UpdateTriggerCommandHandler: {nameof(UpdateTriggerCommandHandler)}");

            await this.ProcessHandlerSequence<UpdateTriggerPostBusDataPacket, UpdateTriggerPostBusSequence, 
                UpdateTriggerCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
