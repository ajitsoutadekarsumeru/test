using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.PermissionSchemesModule
{
    /// <summary>
    /// 
    /// </summary>
    public class LogPermissionSchemeChangesOnAddBusGammaSubscriber : NsbSubscriberBridge<PermissionSchemeAdded>
    {
        readonly ILogger<LogPermissionSchemeChangesOnAddBusGammaSubscriber> _logger;
        readonly ILogPermissionSchemeChangesOnAdd _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public LogPermissionSchemeChangesOnAddBusGammaSubscriber(ILogger<LogPermissionSchemeChangesOnAddBusGammaSubscriber> logger, ILogPermissionSchemeChangesOnAdd subscriber)
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
        public override async Task Handle(PermissionSchemeAdded message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
