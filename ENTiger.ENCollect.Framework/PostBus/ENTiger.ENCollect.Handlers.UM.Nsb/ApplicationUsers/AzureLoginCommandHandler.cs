using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class AzureLoginCommandHandler : NsbCommandHandler<AzureLoginCommand>
    {
        readonly ILogger<AzureLoginCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public AzureLoginCommandHandler(ILogger<AzureLoginCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(AzureLoginCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing AzureLoginCommandHandler: {nameof(AzureLoginCommandHandler)}");

            await this.ProcessHandlerSequence<AzureLoginPostBusDataPacket, AzureLoginPostBusSequence, 
                AzureLoginCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
