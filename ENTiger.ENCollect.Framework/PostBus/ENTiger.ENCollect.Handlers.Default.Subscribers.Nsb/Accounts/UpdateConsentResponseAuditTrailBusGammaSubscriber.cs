using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateConsentResponseAuditTrailBusGammaSubscriber : NsbSubscriberBridge<CustomerConsentUpdated>
    {
        readonly ILogger<UpdateConsentResponseAuditTrailBusGammaSubscriber> _logger;
        readonly IUpdateConsentResponseAuditTrail _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateConsentResponseAuditTrailBusGammaSubscriber(ILogger<UpdateConsentResponseAuditTrailBusGammaSubscriber> logger, IUpdateConsentResponseAuditTrail subscriber)
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
