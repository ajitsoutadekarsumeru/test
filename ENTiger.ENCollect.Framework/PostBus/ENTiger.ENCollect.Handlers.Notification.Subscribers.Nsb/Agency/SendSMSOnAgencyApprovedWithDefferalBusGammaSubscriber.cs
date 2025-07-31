using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendSMSOnAgencyApprovedWithDefferalBusGammaSubscriber : NsbSubscriberBridge<ApproveAgencyWithDefferal>
    {
        readonly ILogger<SendSMSOnAgencyApprovedWithDefferalBusGammaSubscriber> _logger;
        readonly ISendSMSOnAgencyApprovedWithDefferal _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendSMSOnAgencyApprovedWithDefferalBusGammaSubscriber(ILogger<SendSMSOnAgencyApprovedWithDefferalBusGammaSubscriber> logger, ISendSMSOnAgencyApprovedWithDefferal subscriber)
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
        public override async Task Handle(ApproveAgencyWithDefferal message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
