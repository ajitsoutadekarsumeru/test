using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendEmailOnAgencyApprovedBusGammaSubscriber : NsbSubscriberBridge<AgencyApproved>
    {
        readonly ILogger<SendEmailOnAgencyApprovedBusGammaSubscriber> _logger;
        readonly ISendEmailOnAgencyApproved _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendEmailOnAgencyApprovedBusGammaSubscriber(ILogger<SendEmailOnAgencyApprovedBusGammaSubscriber> logger, ISendEmailOnAgencyApproved subscriber)
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
