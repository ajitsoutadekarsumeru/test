using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendSMSOnAgencyApprovedBusGammaSubscriber : NsbSubscriberBridge<AgencyApproved>
    {
        readonly ILogger<SendSMSOnAgencyApprovedBusGammaSubscriber> _logger;
        readonly ISendSMSOnAgencyApproved _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendSMSOnAgencyApprovedBusGammaSubscriber(ILogger<SendSMSOnAgencyApprovedBusGammaSubscriber> logger, ISendSMSOnAgencyApproved subscriber)
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
        public override async Task Handle(AgencyApproved message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
