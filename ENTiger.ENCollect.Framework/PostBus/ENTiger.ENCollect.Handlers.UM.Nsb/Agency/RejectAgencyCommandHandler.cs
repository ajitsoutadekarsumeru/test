using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    /// 
    /// </summary>
    public class RejectAgencyCommandHandler : NsbCommandHandler<RejectAgencyCommand>
    {
        readonly ILogger<RejectAgencyCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public RejectAgencyCommandHandler(ILogger<RejectAgencyCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(RejectAgencyCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing RejectAgencyCommandHandler: {nameof(RejectAgencyCommandHandler)}");

            await this.ProcessHandlerSequence<RejectAgencyPostBusDataPacket, RejectAgencyPostBusSequence, 
                RejectAgencyCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
