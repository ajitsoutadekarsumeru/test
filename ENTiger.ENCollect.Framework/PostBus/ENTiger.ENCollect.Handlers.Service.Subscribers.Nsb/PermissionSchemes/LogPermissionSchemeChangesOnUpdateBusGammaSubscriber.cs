using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.PermissionSchemesModule
{
    /// <summary>
    /// 
    /// </summary>
    public class LogPermissionSchemeChangesOnUpdateBusGammaSubscriber : NsbSubscriberBridge<PermissionSchemeUpdated>
    {
        readonly ILogger<LogPermissionSchemeChangesOnUpdateBusGammaSubscriber> _logger;
        readonly ILogPermissionSchemeChangesOnUpdate _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public LogPermissionSchemeChangesOnUpdateBusGammaSubscriber(ILogger<LogPermissionSchemeChangesOnUpdateBusGammaSubscriber> logger, ILogPermissionSchemeChangesOnUpdate subscriber)
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
        public override async Task Handle(PermissionSchemeUpdated message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
