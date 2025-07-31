using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.FeedbackModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendEmailForBulkTrailProcessedBusGammaSubscriber : NsbSubscriberBridge<BulkTrailProcessedEvent>
    {
        readonly ILogger<SendEmailForBulkTrailProcessedBusGammaSubscriber> _logger;
        readonly ISendEmailForBulkTrailProcessed _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendEmailForBulkTrailProcessedBusGammaSubscriber(ILogger<SendEmailForBulkTrailProcessedBusGammaSubscriber> logger, ISendEmailForBulkTrailProcessed subscriber)
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
        public override async Task Handle(BulkTrailProcessedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
