using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendEmailOnCompanyUserRejectedBusGammaSubscriber : NsbSubscriberBridge<CompanyUserRejected>
    {
        readonly ILogger<SendEmailOnCompanyUserRejectedBusGammaSubscriber> _logger;
        readonly ISendEmailOnCompanyUserRejected _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendEmailOnCompanyUserRejectedBusGammaSubscriber(ILogger<SendEmailOnCompanyUserRejectedBusGammaSubscriber> logger, ISendEmailOnCompanyUserRejected subscriber)
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
        public override async Task Handle(CompanyUserRejected message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
