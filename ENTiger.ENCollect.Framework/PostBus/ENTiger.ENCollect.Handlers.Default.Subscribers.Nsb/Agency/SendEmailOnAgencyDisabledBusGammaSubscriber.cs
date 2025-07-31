using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendEmailOnAgencyDisabledBusGammaSubscriber : NsbSubscriberBridge<AgencyDisabled>
    {
        readonly ILogger<SendEmailOnAgencyDisabledBusGammaSubscriber> _logger;
        readonly ISendEmailOnAgencyDisabled _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendEmailOnAgencyDisabledBusGammaSubscriber(ILogger<SendEmailOnAgencyDisabledBusGammaSubscriber> logger, ISendEmailOnAgencyDisabled subscriber)
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
        public override async Task Handle(AgencyDisabled message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
