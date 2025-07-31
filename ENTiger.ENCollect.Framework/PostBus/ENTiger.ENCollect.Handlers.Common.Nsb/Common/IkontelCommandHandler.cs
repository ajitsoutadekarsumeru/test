using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    /// 
    /// </summary>
    public class IkontelCommandHandler : NsbCommandHandler<IkontelCommand>
    {
        readonly ILogger<IkontelCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public IkontelCommandHandler(ILogger<IkontelCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(IkontelCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing IkontelCommandHandler: {nameof(IkontelCommandHandler)}");

            await this.ProcessHandlerSequence<IkontelPostBusDataPacket, IkontelPostBusSequence, 
                IkontelCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
