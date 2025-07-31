using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AuditTrailModule
{
    /// <summary>
    /// 
    /// </summary>
    public class RecordAuditTrailBusGammaSubscriber : NsbSubscriberBridge<AuditTrailRequestedEvent>
    {
        readonly ILogger<RecordAuditTrailBusGammaSubscriber> _logger;
        readonly IRecordAuditTrail _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public RecordAuditTrailBusGammaSubscriber(ILogger<RecordAuditTrailBusGammaSubscriber> logger, IRecordAuditTrail subscriber)
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
        public override async Task Handle(AuditTrailRequestedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
