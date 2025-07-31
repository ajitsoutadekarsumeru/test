using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public class SendEmailOnCompanyUserMadeDormantBusGammaSubscriber : NsbSubscriberBridge<CompanyUserDormant>
    {
        readonly ILogger<SendEmailOnCompanyUserMadeDormantBusGammaSubscriber> _logger;
        readonly ISendEmailOnCompanyUserMadeDormant _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendEmailOnCompanyUserMadeDormantBusGammaSubscriber(ILogger<SendEmailOnCompanyUserMadeDormantBusGammaSubscriber> logger, ISendEmailOnCompanyUserMadeDormant subscriber)
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
        public override async Task Handle(CompanyUserDormant message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
