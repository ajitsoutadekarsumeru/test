using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    /// 
    /// </summary>
    public class DeactivateAgencyCommandHandler : NsbCommandHandler<DeactivateAgencyCommand>
    {
        readonly ILogger<DeactivateAgencyCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public DeactivateAgencyCommandHandler(ILogger<DeactivateAgencyCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(DeactivateAgencyCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing DeactivateAgencyCommandHandler: {nameof(DeactivateAgencyCommandHandler)}");

            await this.ProcessHandlerSequence<DeactivateAgencyPostBusDataPacket, DeactivateAgencyPostBusSequence, 
                DeactivateAgencyCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
