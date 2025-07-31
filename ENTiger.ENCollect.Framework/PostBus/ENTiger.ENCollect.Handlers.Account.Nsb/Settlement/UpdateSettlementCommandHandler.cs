using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateSettlementCommandHandler : NsbCommandHandler<UpdateSettlementCommand>
    {
        readonly ILogger<UpdateSettlementCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateSettlementCommandHandler(ILogger<UpdateSettlementCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(UpdateSettlementCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing UpdateSettlementCommandHandler: {nameof(UpdateSettlementCommandHandler)}");

            await this.ProcessHandlerSequence<UpdateSettlementPostBusDataPacket, UpdateSettlementPostBusSequence,
                UpdateSettlementCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
