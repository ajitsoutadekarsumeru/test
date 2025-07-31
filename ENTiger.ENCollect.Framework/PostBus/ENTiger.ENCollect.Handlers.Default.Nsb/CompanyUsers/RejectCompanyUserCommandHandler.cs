using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class RejectCompanyUserCommandHandler : NsbCommandHandler<RejectCompanyUserCommand>
    {
        readonly ILogger<RejectCompanyUserCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public RejectCompanyUserCommandHandler(ILogger<RejectCompanyUserCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(RejectCompanyUserCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing RejectCompanyUserCommandHandler: {nameof(RejectCompanyUserCommandHandler)}");

            await this.ProcessHandlerSequence<RejectCompanyUserPostBusDataPacket, RejectCompanyUserPostBusSequence, 
                RejectCompanyUserCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
