using Sumeru.Flex;
using Microsoft.Extensions.Logging;
using ENTiger.ENCollect.AuditTrailModule;


namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    /// 
    /// </summary>
    public class AddAuditTrailCommandHandler : NsbCommandHandler<AddAuditTrailCommand>
    {
        readonly ILogger<AddAuditTrailCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public AddAuditTrailCommandHandler(ILogger<AddAuditTrailCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(AddAuditTrailCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing AddAuditTrailCommandHandler: {nameof(AddAuditTrailCommandHandler)}");

            await this.ProcessHandlerSequence<AddAuditTrailPostBusDataPacket, AddAuditTrailPostBusSequence, 
                AddAuditTrailCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
