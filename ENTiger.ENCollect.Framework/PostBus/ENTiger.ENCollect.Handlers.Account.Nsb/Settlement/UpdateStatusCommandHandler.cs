using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateStatusCommandHandler : NsbCommandHandler<UpdateStatusCommand>
    {
        readonly ILogger<UpdateStatusCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateStatusCommandHandler(ILogger<UpdateStatusCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(UpdateStatusCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing UpdateStatusCommandHandler: {nameof(UpdateStatusCommandHandler)}");

            await this.ProcessHandlerSequence<UpdateStatusPostBusDataPacket, UpdateStatusPostBusSequence, 
                UpdateStatusCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
