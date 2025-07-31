using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendSMSOnAgentRejectedBusGammaSubscriber : NsbSubscriberBridge<AgentRejected>
    {
        readonly ILogger<SendSMSOnAgentRejectedBusGammaSubscriber> _logger;
        readonly ISendSMSOnAgentRejected _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendSMSOnAgentRejectedBusGammaSubscriber(ILogger<SendSMSOnAgentRejectedBusGammaSubscriber> logger, ISendSMSOnAgentRejected subscriber)
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
        public override async Task Handle(AgentRejected message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
