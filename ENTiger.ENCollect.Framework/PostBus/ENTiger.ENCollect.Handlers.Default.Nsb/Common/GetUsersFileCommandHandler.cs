using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    /// 
    /// </summary>
    public class GetUsersFileCommandHandler : NsbCommandHandler<GetUsersFileCommand>
    {
        readonly ILogger<GetUsersFileCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public GetUsersFileCommandHandler(ILogger<GetUsersFileCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(GetUsersFileCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing GetUsersFileCommandHandler: {nameof(GetUsersFileCommandHandler)}");

            await this.ProcessHandlerSequence<GetUsersFilePostBusDataPacket, GetUsersFilePostBusSequence, 
                GetUsersFileCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
