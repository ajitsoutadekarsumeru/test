using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class PrepareCommunicationBusGammaSubscriber : NsbSubscriberBridge<AccountsIdentifiedEvent>
    {
        readonly ILogger<PrepareCommunicationBusGammaSubscriber> _logger;
        readonly IPrepareCommunication _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public PrepareCommunicationBusGammaSubscriber(ILogger<PrepareCommunicationBusGammaSubscriber> logger, IPrepareCommunication subscriber)
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
        public override async Task Handle(AccountsIdentifiedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
