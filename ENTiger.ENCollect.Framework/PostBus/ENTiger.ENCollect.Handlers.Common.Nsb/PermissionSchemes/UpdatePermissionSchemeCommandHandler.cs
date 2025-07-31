using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.PermissionSchemesModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdatePermissionSchemeCommandHandler : NsbCommandHandler<UpdatePermissionSchemeCommand>
    {
        readonly ILogger<UpdatePermissionSchemeCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdatePermissionSchemeCommandHandler(ILogger<UpdatePermissionSchemeCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(UpdatePermissionSchemeCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing UpdatePermissionSchemeCommandHandler: {nameof(UpdatePermissionSchemeCommandHandler)}");

            await this.ProcessHandlerSequence<UpdatePermissionSchemePostBusDataPacket, UpdatePermissionSchemePostBusSequence, 
                UpdatePermissionSchemeCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
