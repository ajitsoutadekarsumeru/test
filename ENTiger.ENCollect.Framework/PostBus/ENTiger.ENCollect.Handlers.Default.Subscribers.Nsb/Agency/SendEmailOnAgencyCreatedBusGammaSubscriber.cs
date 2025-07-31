using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendEmailOnAgencyCreatedBusGammaSubscriber : NsbSubscriberBridge<AgencyCreatedEvent>
    {
        readonly ILogger<SendEmailOnAgencyCreatedBusGammaSubscriber> _logger;
        readonly ISendEmailOnAgencyCreated _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendEmailOnAgencyCreatedBusGammaSubscriber(ILogger<SendEmailOnAgencyCreatedBusGammaSubscriber> logger, ISendEmailOnAgencyCreated subscriber)
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
