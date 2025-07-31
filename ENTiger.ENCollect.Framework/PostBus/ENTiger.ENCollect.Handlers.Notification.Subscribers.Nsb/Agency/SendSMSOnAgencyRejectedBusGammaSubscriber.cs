using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendSMSOnAgencyRejectedBusGammaSubscriber : NsbSubscriberBridge<AgencyRejected>
    {
        readonly ILogger<SendSMSOnAgencyRejectedBusGammaSubscriber> _logger;
        readonly ISendSMSOnAgencyRejected _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendSMSOnAgencyRejectedBusGammaSubscriber(ILogger<SendSMSOnAgencyRejectedBusGammaSubscriber> logger, ISendSMSOnAgencyRejected subscriber)
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
        public override async Task Handle(AgencyRejected message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
