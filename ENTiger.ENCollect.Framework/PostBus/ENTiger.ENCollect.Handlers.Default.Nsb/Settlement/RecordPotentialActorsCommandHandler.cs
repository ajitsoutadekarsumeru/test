using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public class RecordPotentialActorsCommandHandler : NsbCommandHandler<RecordPotentialActorsCommand>
    {
        readonly ILogger<RecordPotentialActorsCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public RecordPotentialActorsCommandHandler(ILogger<RecordPotentialActorsCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(RecordPotentialActorsCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing RecordPotentialActorsCommandHandler: {nameof(RecordPotentialActorsCommandHandler)}");

            await this.ProcessHandlerSequence<RecordPotentialActorsPostBusDataPacket, RecordPotentialActorsPostBusSequence,
                RecordPotentialActorsCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
