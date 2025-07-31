using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class AllocateReceiptOnAgentApprovedBusGammaSubscriber : NsbSubscriberBridge<AgentApproved>
    {
        readonly ILogger<AllocateReceiptOnAgentApprovedBusGammaSubscriber> _logger;
        readonly IAllocateReceiptOnAgentApproved _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public AllocateReceiptOnAgentApprovedBusGammaSubscriber(ILogger<AllocateReceiptOnAgentApprovedBusGammaSubscriber> logger, IAllocateReceiptOnAgentApproved subscriber)
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
