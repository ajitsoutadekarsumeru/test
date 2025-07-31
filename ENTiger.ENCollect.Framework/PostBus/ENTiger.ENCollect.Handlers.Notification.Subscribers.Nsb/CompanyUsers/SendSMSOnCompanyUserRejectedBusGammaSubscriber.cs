using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendSMSOnCompanyUserRejectedBusGammaSubscriber : NsbSubscriberBridge<CompanyUserRejected>
    {
        readonly ILogger<SendSMSOnCompanyUserRejectedBusGammaSubscriber> _logger;
        readonly ISendSMSOnCompanyUserRejected _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendSMSOnCompanyUserRejectedBusGammaSubscriber(ILogger<SendSMSOnCompanyUserRejectedBusGammaSubscriber> logger, ISendSMSOnCompanyUserRejected subscriber)
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
