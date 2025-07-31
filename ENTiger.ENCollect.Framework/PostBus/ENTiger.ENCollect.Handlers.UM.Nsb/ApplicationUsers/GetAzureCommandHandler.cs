using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAzureCommandHandler : NsbCommandHandler<GetAzureCommand>
    {
        readonly ILogger<GetAzureCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public GetAzureCommandHandler(ILogger<GetAzureCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(GetAzureCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing GetAzureCommandHandler: {nameof(GetAzureCommandHandler)}");

            await this.ProcessHandlerSequence<GetAzurePostBusDataPacket, GetAzurePostBusSequence, 
                GetAzureCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
