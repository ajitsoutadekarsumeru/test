using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.PermissionSchemesModule
{
    /// <summary>
    /// 
    /// </summary>
    public class CreatePermissionSchemeCommandHandler : NsbCommandHandler<CreatePermissionSchemeCommand>
    {
        readonly ILogger<CreatePermissionSchemeCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public CreatePermissionSchemeCommandHandler(ILogger<CreatePermissionSchemeCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(CreatePermissionSchemeCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing CreatePermissionSchemeCommandHandler: {nameof(CreatePermissionSchemeCommandHandler)}");

            await this.ProcessHandlerSequence<CreatePermissionSchemePostBusDataPacket, CreatePermissionSchemePostBusSequence, 
                CreatePermissionSchemeCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
