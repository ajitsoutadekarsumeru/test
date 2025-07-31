using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class ChangePasswordCommandHandler : NsbCommandHandler<ChangePasswordCommand>
    {
        readonly ILogger<ChangePasswordCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ChangePasswordCommandHandler(ILogger<ChangePasswordCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(ChangePasswordCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing ChangePasswordCommandHandler: {nameof(ChangePasswordCommandHandler)}");

            await this.ProcessHandlerSequence<ChangePasswordPostBusDataPacket, ChangePasswordPostBusSequence, 
                ChangePasswordCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
