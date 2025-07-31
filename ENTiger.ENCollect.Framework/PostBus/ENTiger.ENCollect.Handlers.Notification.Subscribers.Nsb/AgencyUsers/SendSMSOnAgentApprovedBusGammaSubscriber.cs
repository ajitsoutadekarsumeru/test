using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendSMSOnAgentApprovedBusGammaSubscriber : NsbSubscriberBridge<AgentApproved>
    {
        readonly ILogger<SendSMSOnAgentApprovedBusGammaSubscriber> _logger;
        readonly ISendSMSOnAgentApproved _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendSMSOnAgentApprovedBusGammaSubscriber(ILogger<SendSMSOnAgentApprovedBusGammaSubscriber> logger, ISendSMSOnAgentApproved subscriber)
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
        public override async Task Handle(AgentApproved message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
