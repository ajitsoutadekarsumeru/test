using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.GeoTagModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendEmailForGeoReportBusGammaSubscriber : NsbSubscriberBridge<GeoReportGenerated>
    {
        readonly ILogger<SendEmailForGeoReportBusGammaSubscriber> _logger;
        readonly ISendEmailForGeoReportBusGammaSubscriber _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// 
        public SendEmailForGeoReportBusGammaSubscriber(ILogger<SendEmailForGeoReportBusGammaSubscriber> logger, ISendEmailForGeoReportBusGammaSubscriber subscriber)
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
        public override async Task Handle(GeoReportGenerated message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
