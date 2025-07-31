using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AccountsModule
{
    public class SendSMSForCustomerConsentBusGammaSubscriber : NsbSubscriberBridge<CustomerConsentRequested>
    {
        readonly ILogger<SendSMSForCustomerConsentBusGammaSubscriber> _logger;
        readonly ISendSMSForCustomerConsent _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendSMSForCustomerConsentBusGammaSubscriber(ILogger<SendSMSForCustomerConsentBusGammaSubscriber> logger, ISendSMSForCustomerConsent subscriber)
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
