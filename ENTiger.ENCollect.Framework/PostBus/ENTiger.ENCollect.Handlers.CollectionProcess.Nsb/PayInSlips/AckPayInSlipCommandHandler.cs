using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class AckPayInSlipCommandHandler : NsbCommandHandler<AckPayInSlipCommand>
    {
        readonly ILogger<AckPayInSlipCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public AckPayInSlipCommandHandler(ILogger<AckPayInSlipCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(AckPayInSlipCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing AckPayInSlipCommandHandler: {nameof(AckPayInSlipCommandHandler)}");

            await this.ProcessHandlerSequence<AckPayInSlipPostBusDataPacket, AckPayInSlipPostBusSequence, 
                AckPayInSlipCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
