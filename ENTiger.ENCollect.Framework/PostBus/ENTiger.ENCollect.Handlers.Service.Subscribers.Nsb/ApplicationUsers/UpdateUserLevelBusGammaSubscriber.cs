using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;
using ENTiger.ENCollect.ApplicationUsersModule;
using ENTiger.ENCollect.AgencyUsersModule;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateUserLevelBusGammaSubscriber : NsbSubscriberBridge<UserDesignationChangedEvent>
    {
        readonly ILogger<UpdateUserLevelBusGammaSubscriber> _logger;
        readonly IUpdateUserLevel _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateUserLevelBusGammaSubscriber(ILogger<UpdateUserLevelBusGammaSubscriber> logger,
            IUpdateUserLevel subscriber)
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
        public override async Task Handle(UserDesignationChangedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
