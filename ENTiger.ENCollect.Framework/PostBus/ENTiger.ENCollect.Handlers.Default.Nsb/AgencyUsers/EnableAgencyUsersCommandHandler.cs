using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class EnableAgencyUsersCommandHandler : NsbCommandHandler<EnableAgencyUsersCommand>
    {
        readonly ILogger<EnableAgencyUsersCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public EnableAgencyUsersCommandHandler(ILogger<EnableAgencyUsersCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(EnableAgencyUsersCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing EnableAgencyUsersCommandHandler: {nameof(EnableAgencyUsersCommandHandler)}");

            await this.ProcessHandlerSequence<EnableAgencyUsersPostBusDataPacket, EnableAgencyUsersPostBusSequence, 
                EnableAgencyUsersCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
