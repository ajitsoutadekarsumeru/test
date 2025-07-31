using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class DeactivateCompanyUserCommandHandler : NsbCommandHandler<DeactivateCompanyUserCommand>
    {
        readonly ILogger<DeactivateCompanyUserCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public DeactivateCompanyUserCommandHandler(ILogger<DeactivateCompanyUserCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(DeactivateCompanyUserCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing DeactivateCompanyUserCommandHandler: {nameof(DeactivateCompanyUserCommandHandler)}");

            await this.ProcessHandlerSequence<DeactivateCompanyUserPostBusDataPacket, DeactivateCompanyUserPostBusSequence, 
                DeactivateCompanyUserCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
