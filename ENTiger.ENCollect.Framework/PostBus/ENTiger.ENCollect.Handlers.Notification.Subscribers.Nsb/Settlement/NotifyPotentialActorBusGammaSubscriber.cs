using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;
using ENTiger.ENCollect.ApplicationUsersModule;
using ENTiger.ENCollect.AgencyUsersModule;
using ENCollect.Dyna.Workflows;

namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public class NotifyPotentialActorBusGammaSubscriber : NsbSubscriberBridge<ActorIndentifiedEvent>
    {
        readonly ILogger<NotifyPotentialActorBusGammaSubscriber> _logger;
        readonly INotifyPotentialActor _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public NotifyPotentialActorBusGammaSubscriber(ILogger<NotifyPotentialActorBusGammaSubscriber> logger,
            INotifyPotentialActor subscriber)
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
        public override async Task Handle(ActorIndentifiedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
