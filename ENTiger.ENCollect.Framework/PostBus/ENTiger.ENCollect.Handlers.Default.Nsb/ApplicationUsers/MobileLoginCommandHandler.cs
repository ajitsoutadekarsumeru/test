using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class MobileLoginCommandHandler : NsbCommandHandler<MobileLoginCommand>
    {
        readonly ILogger<MobileLoginCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public MobileLoginCommandHandler(ILogger<MobileLoginCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(MobileLoginCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing MobileLoginCommandHandler: {nameof(MobileLoginCommandHandler)}");

            await this.ProcessHandlerSequence<MobileLoginPostBusDataPacket, MobileLoginPostBusSequence, 
                MobileLoginCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
