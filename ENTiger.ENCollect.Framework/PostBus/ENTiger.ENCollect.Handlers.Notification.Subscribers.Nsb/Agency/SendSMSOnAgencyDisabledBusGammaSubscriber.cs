using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendSMSOnAgencyDisabledBusGammaSubscriber : NsbSubscriberBridge<AgencyDisabled>
    {
        readonly ILogger<SendSMSOnAgencyDisabledBusGammaSubscriber> _logger;
        readonly ISendSMSOnAgencyDisabled _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendSMSOnAgencyDisabledBusGammaSubscriber(ILogger<SendSMSOnAgencyDisabledBusGammaSubscriber> logger, ISendSMSOnAgencyDisabled subscriber)
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
