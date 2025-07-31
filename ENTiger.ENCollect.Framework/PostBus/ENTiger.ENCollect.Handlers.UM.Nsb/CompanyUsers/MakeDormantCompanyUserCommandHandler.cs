using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public class MakeDormantCompanyUserCommandHandler : NsbCommandHandler<MakeDormantCompanyUserCommand>
    {
        readonly ILogger<MakeDormantCompanyUserCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public MakeDormantCompanyUserCommandHandler(ILogger<MakeDormantCompanyUserCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(MakeDormantCompanyUserCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing MakeDormantCompanyUserCommandHandler: {nameof(MakeDormantCompanyUserCommandHandler)}");

            await this.ProcessHandlerSequence<MakeDormantCompanyUserPostBusDataPacket, MakeDormantCompanyUserPostBusSequence,
                MakeDormantCompanyUserCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
