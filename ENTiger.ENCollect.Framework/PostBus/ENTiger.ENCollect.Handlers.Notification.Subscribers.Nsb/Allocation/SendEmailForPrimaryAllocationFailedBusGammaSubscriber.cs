using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendEmailForPrimaryAllocationFailedBusGammaSubscriber : NsbSubscriberBridge<PrimaryAllocationFailedEvent>
    {
        readonly ILogger<SendEmailForPrimaryAllocationFailedBusGammaSubscriber> _logger;
        readonly ISendEmailForPrimaryAllocationFailed _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendEmailForPrimaryAllocationFailedBusGammaSubscriber(ILogger<SendEmailForPrimaryAllocationFailedBusGammaSubscriber> logger, ISendEmailForPrimaryAllocationFailed subscriber)
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
        public override async Task Handle(PrimaryAllocationFailedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
