using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendEmailOnAgentApprovedBusGammaSubscriber : NsbSubscriberBridge<AgentApproved>
    {
        readonly ILogger<SendEmailOnAgentApprovedBusGammaSubscriber> _logger;
        readonly ISendEmailOnAgentApproved _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendEmailOnAgentApprovedBusGammaSubscriber(ILogger<SendEmailOnAgentApprovedBusGammaSubscriber> logger, ISendEmailOnAgentApproved subscriber)
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
