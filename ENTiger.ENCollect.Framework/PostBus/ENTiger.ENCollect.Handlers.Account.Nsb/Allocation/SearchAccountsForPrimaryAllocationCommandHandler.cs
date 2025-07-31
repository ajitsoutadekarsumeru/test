using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SearchAccountsForPrimaryAllocationCommandHandler : NsbCommandHandler<SearchAccountsForPrimaryAllocationCommand>
    {
        readonly ILogger<SearchAccountsForPrimaryAllocationCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SearchAccountsForPrimaryAllocationCommandHandler(ILogger<SearchAccountsForPrimaryAllocationCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(SearchAccountsForPrimaryAllocationCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing SearchAccountsForPrimaryAllocationCommandHandler: {nameof(SearchAccountsForPrimaryAllocationCommandHandler)}");

            await this.ProcessHandlerSequence<SearchAccountsForPrimaryAllocationPostBusDataPacket, SearchAccountsForPrimaryAllocationPostBusSequence, 
                SearchAccountsForPrimaryAllocationCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
