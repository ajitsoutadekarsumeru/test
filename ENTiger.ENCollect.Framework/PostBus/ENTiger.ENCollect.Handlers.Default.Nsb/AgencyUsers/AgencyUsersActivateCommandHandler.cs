using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class AgencyUsersActivateCommandHandler : NsbCommandHandler<AgencyUsersActivateCommand>
    {
        readonly ILogger<AgencyUsersActivateCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public AgencyUsersActivateCommandHandler(ILogger<AgencyUsersActivateCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(AgencyUsersActivateCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing AgencyUsersActivateCommandHandler: {nameof(AgencyUsersActivateCommandHandler)}");

            await this.ProcessHandlerSequence<AgencyUsersActivatePostBusDataPacket, AgencyUsersActivatePostBusSequence, 
                AgencyUsersActivateCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
