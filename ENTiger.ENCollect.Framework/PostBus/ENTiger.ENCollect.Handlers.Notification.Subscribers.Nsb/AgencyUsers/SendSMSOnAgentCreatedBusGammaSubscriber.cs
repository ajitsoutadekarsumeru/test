using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendSMSOnAgentCreatedBusGammaSubscriber : NsbSubscriberBridge<AgentAddedEvent>
    {
        readonly ILogger<SendSMSOnAgentCreatedBusGammaSubscriber> _logger;
        readonly ISendSMSOnAgentCreated _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendSMSOnAgentCreatedBusGammaSubscriber(ILogger<SendSMSOnAgentCreatedBusGammaSubscriber> logger, ISendSMSOnAgentCreated subscriber)
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
        public override async Task Handle(AgentAddedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
