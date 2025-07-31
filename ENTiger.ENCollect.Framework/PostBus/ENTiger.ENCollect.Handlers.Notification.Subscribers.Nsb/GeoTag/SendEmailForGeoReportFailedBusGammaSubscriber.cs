using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.GeoTagModule
{
    public class SendEmailForGeoReportFailedBusGammaSubscriber : NsbSubscriberBridge<GeoReportFailed>
    {
        readonly ILogger<SendEmailForGeoReportFailedBusGammaSubscriber> _logger;
        readonly ISendEmailForGeoReportFailedBusGammaSubscriber _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// 
        public SendEmailForGeoReportFailedBusGammaSubscriber(ILogger<SendEmailForGeoReportFailedBusGammaSubscriber> logger, ISendEmailForGeoReportFailedBusGammaSubscriber subscriber)
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
        public override async Task Handle(GeoReportFailed message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}