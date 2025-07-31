using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.PublicModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdatePayuResponseCommandHandler : NsbCommandHandler<UpdatePayuResponseCommand>
    {
        readonly ILogger<UpdatePayuResponseCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdatePayuResponseCommandHandler(ILogger<UpdatePayuResponseCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(UpdatePayuResponseCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing UpdatePayuResponseCommandHandler: {nameof(UpdatePayuResponseCommandHandler)}");

            await this.ProcessHandlerSequence<UpdatePayuResponsePostBusDataPacket, UpdatePayuResponsePostBusSequence, 
                UpdatePayuResponseCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
