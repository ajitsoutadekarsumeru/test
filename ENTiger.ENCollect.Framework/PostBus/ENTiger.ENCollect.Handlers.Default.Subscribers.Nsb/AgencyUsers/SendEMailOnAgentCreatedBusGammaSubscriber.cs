using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendEMailOnAgentCreatedBusGammaSubscriber : NsbSubscriberBridge<AgentAddedEvent>
    {
        readonly ILogger<SendEMailOnAgentCreatedBusGammaSubscriber> _logger;
        readonly ISendEMailOnAgentCreated _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendEMailOnAgentCreatedBusGammaSubscriber(ILogger<SendEMailOnAgentCreatedBusGammaSubscriber> logger, ISendEMailOnAgentCreated subscriber)
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
