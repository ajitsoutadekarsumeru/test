using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateMastersCommandHandler : NsbCommandHandler<UpdateMastersCommand>
    {
        readonly ILogger<UpdateMastersCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateMastersCommandHandler(ILogger<UpdateMastersCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(UpdateMastersCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing UpdateMastersCommandHandler: {nameof(UpdateMastersCommandHandler)}");

            await this.ProcessHandlerSequence<UpdateMastersPostBusDataPacket, UpdateMastersPostBusSequence, 
                UpdateMastersCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
