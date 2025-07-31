using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    /// 
    /// </summary>
    public class ApproveCollectionAgencyCommandHandler : NsbCommandHandler<ApproveCollectionAgencyCommand>
    {
        readonly ILogger<ApproveCollectionAgencyCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ApproveCollectionAgencyCommandHandler(ILogger<ApproveCollectionAgencyCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(ApproveCollectionAgencyCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing ApproveCollectionAgencyCommandHandler: {nameof(ApproveCollectionAgencyCommandHandler)}");

            await this.ProcessHandlerSequence<ApproveCollectionAgencyPostBusDataPacket, ApproveCollectionAgencyPostBusSequence, 
                ApproveCollectionAgencyCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
