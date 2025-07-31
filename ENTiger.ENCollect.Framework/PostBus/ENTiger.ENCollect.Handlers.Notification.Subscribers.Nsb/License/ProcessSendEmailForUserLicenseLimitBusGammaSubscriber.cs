using ENTiger.ENCollect.ApplicationUsersModule;
using ENTiger.ENCollect.Messages.Events.License;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    /// 
    /// </summary>
    public class ProcessSendEmailForUserLicenseLimitBusGammaSubscriber : NsbSubscriberBridge<UserLicenseLimitReachedEvent>
    {
        readonly ILogger<ProcessSendEmailForUserLicenseLimitBusGammaSubscriber> _logger;
        readonly ISendEmailForLicenseUserLimit _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ProcessSendEmailForUserLicenseLimitBusGammaSubscriber(ILogger<ProcessSendEmailForUserLicenseLimitBusGammaSubscriber> logger, ISendEmailForLicenseUserLimit subscriber)
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
        public override async Task Handle(UserLicenseLimitReachedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
