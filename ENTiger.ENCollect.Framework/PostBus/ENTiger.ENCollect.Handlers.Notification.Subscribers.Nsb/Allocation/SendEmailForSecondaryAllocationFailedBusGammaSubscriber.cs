using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendEmailForSecondaryAllocationFailedBusGammaSubscriber : NsbSubscriberBridge<SecondaryAllocationFailedEvent>
    {
        readonly ILogger<SendEmailForSecondaryAllocationFailedBusGammaSubscriber> _logger;
        readonly ISendEmailForSecondaryAllocationFailed _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendEmailForSecondaryAllocationFailedBusGammaSubscriber(ILogger<SendEmailForSecondaryAllocationFailedBusGammaSubscriber> logger, ISendEmailForSecondaryAllocationFailed subscriber)
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
        public override async Task Handle(SecondaryAllocationFailedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
