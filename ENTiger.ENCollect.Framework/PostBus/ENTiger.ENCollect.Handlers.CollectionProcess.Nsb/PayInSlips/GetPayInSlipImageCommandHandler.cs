using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class GetPayInSlipImageCommandHandler : NsbCommandHandler<GetPayInSlipImageCommand>
    {
        readonly ILogger<GetPayInSlipImageCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public GetPayInSlipImageCommandHandler(ILogger<GetPayInSlipImageCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(GetPayInSlipImageCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing GetPayInSlipImageCommandHandler: {nameof(GetPayInSlipImageCommandHandler)}");

            await this.ProcessHandlerSequence<GetPayInSlipImagePostBusDataPacket, GetPayInSlipImagePostBusSequence, 
                GetPayInSlipImageCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
