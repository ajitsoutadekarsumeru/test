using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateUsersByBatchCommandHandler : NsbCommandHandler<UpdateUsersByBatchCommand>
    {
        readonly ILogger<UpdateUsersByBatchCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateUsersByBatchCommandHandler(ILogger<UpdateUsersByBatchCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(UpdateUsersByBatchCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing UpdateUsersByBatchCommandHandler: {nameof(UpdateUsersByBatchCommandHandler)}");

            await this.ProcessHandlerSequence<UpdateUsersByBatchPostBusDataPacket, UpdateUsersByBatchPostBusSequence, 
                UpdateUsersByBatchCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
