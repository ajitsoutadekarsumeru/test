using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    /// 
    /// </summary>
    public class ApproveAgencyWithDeferralCommandHandler : NsbCommandHandler<ApproveAgencyWithDeferralCommand>
    {
        readonly ILogger<ApproveAgencyWithDeferralCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ApproveAgencyWithDeferralCommandHandler(ILogger<ApproveAgencyWithDeferralCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(ApproveAgencyWithDeferralCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing ApproveAgencyWithDeferralCommandHandler: {nameof(ApproveAgencyWithDeferralCommandHandler)}");

            await this.ProcessHandlerSequence<ApproveAgencyWithDeferralPostBusDataPacket, ApproveAgencyWithDeferralPostBusSequence, 
                ApproveAgencyWithDeferralCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
