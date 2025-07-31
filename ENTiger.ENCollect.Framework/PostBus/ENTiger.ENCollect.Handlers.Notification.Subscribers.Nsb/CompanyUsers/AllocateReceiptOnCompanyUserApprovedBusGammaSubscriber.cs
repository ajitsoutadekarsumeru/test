using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class AllocateReceiptOnCompanyUserApprovedBusGammaSubscriber : NsbSubscriberBridge<CompanyUserApproved>
    {
        readonly ILogger<AllocateReceiptOnCompanyUserApprovedBusGammaSubscriber> _logger;
        readonly IAllocateReceiptOnCompanyUserApproved _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public AllocateReceiptOnCompanyUserApprovedBusGammaSubscriber(ILogger<AllocateReceiptOnCompanyUserApprovedBusGammaSubscriber> logger, IAllocateReceiptOnCompanyUserApproved subscriber)
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
