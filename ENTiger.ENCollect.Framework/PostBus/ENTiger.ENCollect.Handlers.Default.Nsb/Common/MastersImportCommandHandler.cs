using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    /// 
    /// </summary>
    public class MastersImportCommandHandler : NsbCommandHandler<MastersImportCommand>
    {
        readonly ILogger<MastersImportCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public MastersImportCommandHandler(ILogger<MastersImportCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(MastersImportCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing MastersImportCommandHandler: {nameof(MastersImportCommandHandler)}");

            await this.ProcessHandlerSequence<MastersImportPostBusDataPacket, MastersImportPostBusSequence, 
                MastersImportCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
