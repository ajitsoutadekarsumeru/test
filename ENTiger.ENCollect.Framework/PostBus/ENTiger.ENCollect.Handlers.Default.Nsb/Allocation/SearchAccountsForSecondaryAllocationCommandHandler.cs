using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SearchAccountsForSecondaryAllocationCommandHandler : NsbCommandHandler<SearchAccountsForSecondaryAllocationCommand>
    {
        readonly ILogger<SearchAccountsForSecondaryAllocationCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SearchAccountsForSecondaryAllocationCommandHandler(ILogger<SearchAccountsForSecondaryAllocationCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(SearchAccountsForSecondaryAllocationCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing SearchAccountsForSecondaryAllocationCommandHandler: {nameof(SearchAccountsForSecondaryAllocationCommandHandler)}");

            await this.ProcessHandlerSequence<SearchAccountsForSecondaryAllocationPostBusDataPacket, SearchAccountsForSecondaryAllocationPostBusSequence, 
                SearchAccountsForSecondaryAllocationCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
