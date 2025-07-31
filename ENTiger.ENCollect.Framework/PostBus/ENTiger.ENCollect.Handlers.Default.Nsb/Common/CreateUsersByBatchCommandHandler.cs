using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateUsersByBatchCommandHandler : NsbCommandHandler<CreateUsersByBatchCommand>
    {
        readonly ILogger<CreateUsersByBatchCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public CreateUsersByBatchCommandHandler(ILogger<CreateUsersByBatchCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(CreateUsersByBatchCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing CreateUsersByBatchCommandHandler: {nameof(CreateUsersByBatchCommandHandler)}");

            await this.ProcessHandlerSequence<CreateUsersByBatchPostBusDataPacket, CreateUsersByBatchPostBusSequence, 
                CreateUsersByBatchCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
