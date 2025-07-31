using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    /// 
    /// </summary>
    public class RenewAgencyCommandHandler : NsbCommandHandler<RenewAgencyCommand>
    {
        readonly ILogger<RenewAgencyCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public RenewAgencyCommandHandler(ILogger<RenewAgencyCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(RenewAgencyCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing RenewAgencyCommandHandler: {nameof(RenewAgencyCommandHandler)}");

            await this.ProcessHandlerSequence<RenewAgencyPostBusDataPacket, RenewAgencyPostBusSequence, 
                RenewAgencyCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
