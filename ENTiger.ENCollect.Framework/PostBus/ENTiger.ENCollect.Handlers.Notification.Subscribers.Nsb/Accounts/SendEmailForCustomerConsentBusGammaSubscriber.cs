using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendEmailForCustomerConsentBusGammaSubscriber : NsbSubscriberBridge<CustomerConsentRequested>
    {
        readonly ILogger<SendEmailForCustomerConsentBusGammaSubscriber> _logger;
        readonly ISendEmailForCustomerConsent _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendEmailForCustomerConsentBusGammaSubscriber(ILogger<SendEmailForCustomerConsentBusGammaSubscriber> logger, ISendEmailForCustomerConsent subscriber)
        {
            _logger = logger;
            _subscriber = subscriber;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task Handle(CustomerConsentRequested message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
