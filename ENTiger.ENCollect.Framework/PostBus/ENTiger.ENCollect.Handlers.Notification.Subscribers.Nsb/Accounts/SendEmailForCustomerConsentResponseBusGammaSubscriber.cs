using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AccountsModule
{
    public class SendEmailForCustomerConsentResponseBusGammaSubscriber : NsbSubscriberBridge<CustomerConsentUpdated>
    {
        readonly ILogger<SendEmailForCustomerConsentResponseBusGammaSubscriber> _logger;
        readonly ISendEmailForCustomerConsentResponse _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendEmailForCustomerConsentResponseBusGammaSubscriber(ILogger<SendEmailForCustomerConsentResponseBusGammaSubscriber> logger, ISendEmailForCustomerConsentResponse subscriber)
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
        public override async Task Handle(CustomerConsentUpdated message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
