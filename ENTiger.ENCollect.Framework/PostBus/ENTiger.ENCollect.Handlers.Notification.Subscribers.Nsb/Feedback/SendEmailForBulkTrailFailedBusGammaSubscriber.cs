using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.FeedbackModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendEmailForBulkTrailFailedBusGammaSubscriber : NsbSubscriberBridge<BulkTrailFailedEvent>
    {
        readonly ILogger<SendEmailForBulkTrailFailedBusGammaSubscriber> _logger;
        readonly ISendEmailForBulkTrailFailed _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendEmailForBulkTrailFailedBusGammaSubscriber(ILogger<SendEmailForBulkTrailFailedBusGammaSubscriber> logger, ISendEmailForBulkTrailFailed subscriber)
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
        public override async Task Handle(BulkTrailFailedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
