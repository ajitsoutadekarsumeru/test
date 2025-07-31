using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class ADLoginCommandHandler : NsbCommandHandler<ADLoginCommand>
    {
        readonly ILogger<ADLoginCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ADLoginCommandHandler(ILogger<ADLoginCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(ADLoginCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing ADLoginCommandHandler: {nameof(ADLoginCommandHandler)}");

            await this.ProcessHandlerSequence<ADLoginPostBusDataPacket, ADLoginPostBusSequence, 
                ADLoginCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
