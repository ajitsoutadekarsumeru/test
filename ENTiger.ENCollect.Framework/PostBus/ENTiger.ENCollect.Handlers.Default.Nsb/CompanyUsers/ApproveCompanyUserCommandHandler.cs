using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class ApproveCompanyUserCommandHandler : NsbCommandHandler<ApproveCompanyUserCommand>
    {
        readonly ILogger<ApproveCompanyUserCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ApproveCompanyUserCommandHandler(ILogger<ApproveCompanyUserCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(ApproveCompanyUserCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing ApproveCompanyUserCommandHandler: {nameof(ApproveCompanyUserCommandHandler)}");

            await this.ProcessHandlerSequence<ApproveCompanyUserPostBusDataPacket, ApproveCompanyUserPostBusSequence, 
                ApproveCompanyUserCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
