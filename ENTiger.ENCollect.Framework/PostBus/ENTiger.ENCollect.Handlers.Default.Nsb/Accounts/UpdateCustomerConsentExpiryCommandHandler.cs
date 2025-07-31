using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.AccountsModule
{
    public class UpdateCustomerConsentExpiryCommandHandler : NsbCommandHandler<UpdateCustomerConsentExpiryCommand>
    {
        readonly ILogger<UpdateCustomerConsentExpiryCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateCustomerConsentExpiryCommandHandler(ILogger<UpdateCustomerConsentExpiryCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(UpdateCustomerConsentExpiryCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing UpdateCustomerConsentExpiryCommandHandler: {nameof(UpdateCustomerConsentExpiryCommandHandler)}");

            await this.ProcessHandlerSequence<UpdateCustomerConsentExpiryPostBusDataPacket, UpdateCustomerConsentExpiryPostBusSequence, 
                UpdateCustomerConsentExpiryCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
