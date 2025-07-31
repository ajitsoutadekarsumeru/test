using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAccountBalanceDetailsCommandHandler : NsbCommandHandler<GetAccountBalanceDetailsCommand>
    {
        readonly ILogger<GetAccountBalanceDetailsCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public GetAccountBalanceDetailsCommandHandler(ILogger<GetAccountBalanceDetailsCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(GetAccountBalanceDetailsCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing GetAccountBalanceDetailsCommandHandler: {nameof(GetAccountBalanceDetailsCommandHandler)}");

            await this.ProcessHandlerSequence<GetAccountBalanceDetailsPostBusDataPacket, GetAccountBalanceDetailsPostBusSequence, 
                GetAccountBalanceDetailsCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
