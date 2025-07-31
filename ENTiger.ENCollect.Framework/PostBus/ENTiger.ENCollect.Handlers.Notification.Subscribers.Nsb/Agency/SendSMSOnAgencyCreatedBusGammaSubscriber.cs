using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendSMSOnAgencyCreatedBusGammaSubscriber : NsbSubscriberBridge<AgencyCreatedEvent>
    {
        readonly ILogger<SendSMSOnAgencyCreatedBusGammaSubscriber> _logger;
        readonly ISendSMSOnAgencyCreated _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendSMSOnAgencyCreatedBusGammaSubscriber(ILogger<SendSMSOnAgencyCreatedBusGammaSubscriber> logger, ISendSMSOnAgencyCreated subscriber)
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
        public override async Task Handle(AgencyCreatedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
