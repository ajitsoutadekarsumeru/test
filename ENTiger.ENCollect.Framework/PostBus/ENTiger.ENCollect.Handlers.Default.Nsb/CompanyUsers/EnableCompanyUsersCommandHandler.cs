using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class EnableCompanyUsersCommandHandler : NsbCommandHandler<EnableCompanyUsersCommand>
    {
        readonly ILogger<EnableCompanyUsersCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public EnableCompanyUsersCommandHandler(ILogger<EnableCompanyUsersCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(EnableCompanyUsersCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing EnableCompanyUsersCommandHandler: {nameof(EnableCompanyUsersCommandHandler)}");

            await this.ProcessHandlerSequence<EnableCompanyUsersPostBusDataPacket, EnableCompanyUsersPostBusSequence, 
                EnableCompanyUsersCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
