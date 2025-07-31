using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateRoleBasedSearchCommandHandler : NsbCommandHandler<UpdateAccountScopeConfigurationCommand>
    {
        readonly ILogger<UpdateRoleBasedSearchCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateRoleBasedSearchCommandHandler(ILogger<UpdateRoleBasedSearchCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(UpdateAccountScopeConfigurationCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing UpdateRoleBasedSearchCommandHandler: {nameof(UpdateRoleBasedSearchCommandHandler)}");

            await this.ProcessHandlerSequence<UpdateAccountScopeConfigurationPostBusDataPacket, UpdateRoleBasedSearchPostBusSequence, 
                UpdateAccountScopeConfigurationCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
