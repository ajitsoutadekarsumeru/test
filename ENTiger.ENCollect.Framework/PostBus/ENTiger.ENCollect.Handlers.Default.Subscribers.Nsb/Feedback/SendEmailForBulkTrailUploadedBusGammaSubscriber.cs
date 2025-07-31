using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.FeedbackModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendEmailForBulkTrailUploadedBusGammaSubscriber : NsbSubscriberBridge<BulkTrailUploadedEvent>
    {
        readonly ILogger<SendEmailForBulkTrailUploadedBusGammaSubscriber> _logger;
        readonly ISendEmailForBulkTrailUploaded _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendEmailForBulkTrailUploadedBusGammaSubscriber(ILogger<SendEmailForBulkTrailUploadedBusGammaSubscriber> logger, ISendEmailForBulkTrailUploaded subscriber)
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
        public override async Task Handle(BulkTrailUploadedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
