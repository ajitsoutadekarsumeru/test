using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class RequestCustomerConsentCommandHandler : NsbCommandHandler<RequestCustomerConsentCommand>
    {
        readonly ILogger<RequestCustomerConsentCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public RequestCustomerConsentCommandHandler(ILogger<RequestCustomerConsentCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(RequestCustomerConsentCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing RequestCustomerConsentCommandHandler: {nameof(RequestCustomerConsentCommandHandler)}");

            await this.ProcessHandlerSequence<RequestCustomerConsentPostBusDataPacket, RequestCustomerConsentPostBusSequence, 
                RequestCustomerConsentCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
