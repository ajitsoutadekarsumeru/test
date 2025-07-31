using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public class SendEmailOnAgencyUserMadeDormantBusGammaSubscriber : NsbSubscriberBridge<AgentDormant>
    {
        readonly ILogger<SendEmailOnAgencyUserMadeDormantBusGammaSubscriber> _logger;
        readonly ISendEmailOnAgencyUserMadeDormant _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendEmailOnAgencyUserMadeDormantBusGammaSubscriber(ILogger<SendEmailOnAgencyUserMadeDormantBusGammaSubscriber> logger, ISendEmailOnAgencyUserMadeDormant subscriber)
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
        public override async Task Handle(AgentDormant message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
