using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendSMSOnCompanyUserCreatedBusGammaSubscriber : NsbSubscriberBridge<CompanyUserCreatedEvent>
    {
        readonly ILogger<SendSMSOnCompanyUserCreatedBusGammaSubscriber> _logger;
        readonly ISendSMSOnCompanyUserCreated _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendSMSOnCompanyUserCreatedBusGammaSubscriber(ILogger<SendSMSOnCompanyUserCreatedBusGammaSubscriber> logger, ISendSMSOnCompanyUserCreated subscriber)
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
        public override async Task Handle(CompanyUserCreatedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
