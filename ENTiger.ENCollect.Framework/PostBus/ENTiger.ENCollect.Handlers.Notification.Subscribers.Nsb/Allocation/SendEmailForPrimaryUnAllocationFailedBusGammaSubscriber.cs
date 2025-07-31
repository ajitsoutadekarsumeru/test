using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendEmailForPrimaryUnAllocationFailedBusGammaSubscriber : NsbSubscriberBridge<PrimaryUnAllocationFailedEvent>
    {
        readonly ILogger<SendEmailForPrimaryUnAllocationFailedBusGammaSubscriber> _logger;
        readonly ISendEmailForPrimaryUnAllocationFailed _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendEmailForPrimaryUnAllocationFailedBusGammaSubscriber(ILogger<SendEmailForPrimaryUnAllocationFailedBusGammaSubscriber> logger, ISendEmailForPrimaryUnAllocationFailed subscriber)
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
        public override async Task Handle(PrimaryUnAllocationFailedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
