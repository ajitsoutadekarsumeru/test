using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class CompanyUsersActivateCommandHandler : NsbCommandHandler<CompanyUsersActivateCommand>
    {
        readonly ILogger<CompanyUsersActivateCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public CompanyUsersActivateCommandHandler(ILogger<CompanyUsersActivateCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(CompanyUsersActivateCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing CompanyUsersActivateCommandHandler: {nameof(CompanyUsersActivateCommandHandler)}");

            await this.ProcessHandlerSequence<CompanyUsersActivatePostBusDataPacket, CompanyUsersActivatePostBusSequence, 
                CompanyUsersActivateCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
