using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class PrintIdCardCommandHandler : NsbCommandHandler<PrintIdCardCommand>
    {
        readonly ILogger<PrintIdCardCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public PrintIdCardCommandHandler(ILogger<PrintIdCardCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(PrintIdCardCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing PrintIdCardCommandHandler: {nameof(PrintIdCardCommandHandler)}");

            await this.ProcessHandlerSequence<PrintIdCardPostBusDataPacket, PrintIdCardPostBusSequence, 
                PrintIdCardCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
