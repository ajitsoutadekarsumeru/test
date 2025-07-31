using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.FeedbackModule
{
    /// <summary>
    /// 
    /// </summary>
    public class ProcessBulkTrailUploadedBusGammaSubscriber : NsbSubscriberBridge<BulkTrailUploadedEvent>
    {
        readonly ILogger<ProcessBulkTrailUploadedBusGammaSubscriber> _logger;
        readonly IProcessBulkTrailUploaded _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ProcessBulkTrailUploadedBusGammaSubscriber(ILogger<ProcessBulkTrailUploadedBusGammaSubscriber> logger, IProcessBulkTrailUploaded subscriber)
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
