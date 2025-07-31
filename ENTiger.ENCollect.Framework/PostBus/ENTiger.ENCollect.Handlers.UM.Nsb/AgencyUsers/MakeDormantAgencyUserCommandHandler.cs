using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public class MakeDormantAgencyUserCommandHandler : NsbCommandHandler<MakeDormantAgencyUserCommand>
    {
        readonly ILogger<MakeDormantAgencyUserCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public MakeDormantAgencyUserCommandHandler(ILogger<MakeDormantAgencyUserCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(MakeDormantAgencyUserCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing MakeDormantAgencyUserCommandHandler: {nameof(MakeDormantAgencyUserCommandHandler)}");

            await this.ProcessHandlerSequence<MakeDormantAgencyUserPostBusDataPacket, MakeDormantAgencyUserPostBusSequence,
                MakeDormantAgencyUserCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
