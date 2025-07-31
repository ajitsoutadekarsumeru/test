using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendSMSOnCompanyUserApprovedBusGammaSubscriber : NsbSubscriberBridge<CompanyUserApproved>
    {
        readonly ILogger<SendSMSOnCompanyUserApprovedBusGammaSubscriber> _logger;
        readonly ISendSMSOnCompanyUserApproved _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendSMSOnCompanyUserApprovedBusGammaSubscriber(ILogger<SendSMSOnCompanyUserApprovedBusGammaSubscriber> logger, ISendSMSOnCompanyUserApproved subscriber)
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
        public override async Task Handle(CompanyUserApproved message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
