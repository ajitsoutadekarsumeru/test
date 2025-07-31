using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class AddTriggerCommandHandler : NsbCommandHandler<AddTriggerCommand>
    {
        readonly ILogger<AddTriggerCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public AddTriggerCommandHandler(ILogger<AddTriggerCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(AddTriggerCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing AddTriggerCommandHandler: {nameof(AddTriggerCommandHandler)}");

            await this.ProcessHandlerSequence<AddTriggerPostBusDataPacket, AddTriggerPostBusSequence, 
                AddTriggerCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
