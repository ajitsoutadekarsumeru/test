using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateACMCommandHandler : NsbCommandHandler<UpdateACMCommand>
    {
        readonly ILogger<UpdateACMCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateACMCommandHandler(ILogger<UpdateACMCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(UpdateACMCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing UpdateACMCommandHandler: {nameof(UpdateACMCommandHandler)}");

            await this.ProcessHandlerSequence<UpdateACMPostBusDataPacket, UpdateACMPostBusSequence, 
                UpdateACMCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
