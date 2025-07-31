using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateConsentRequestAuditTrailBusGammaSubscriber : NsbSubscriberBridge<CustomerConsentRequested>
    {
        readonly ILogger<UpdateConsentRequestAuditTrailBusGammaSubscriber> _logger;
        readonly IUpdateConsentRequestAuditTrail _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateConsentRequestAuditTrailBusGammaSubscriber(ILogger<UpdateConsentRequestAuditTrailBusGammaSubscriber> logger, IUpdateConsentRequestAuditTrail subscriber)
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
