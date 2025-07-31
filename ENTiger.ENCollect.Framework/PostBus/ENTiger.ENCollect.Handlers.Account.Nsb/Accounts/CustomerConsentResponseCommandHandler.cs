using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public class CustomerConsentResponseCommandHandler : NsbCommandHandler<CustomerConsentResponseCommand>
    {
        readonly ILogger<CustomerConsentResponseCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public CustomerConsentResponseCommandHandler(ILogger<CustomerConsentResponseCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(CustomerConsentResponseCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing CustomerConsentResponseCommandHandler: {nameof(CustomerConsentResponseCommandHandler)}");

            await this.ProcessHandlerSequence<CustomerConsentResponsePostBusDataPacket, CustomerConsentResponsePostBusSequence,
                CustomerConsentResponseCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
