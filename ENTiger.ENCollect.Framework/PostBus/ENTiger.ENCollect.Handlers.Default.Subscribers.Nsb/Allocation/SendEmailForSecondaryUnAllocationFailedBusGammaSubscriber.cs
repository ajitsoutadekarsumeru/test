using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendEmailForSecondaryUnAllocationFailedBusGammaSubscriber : NsbSubscriberBridge<SecondaryUnAllocationFailedEvent>
    {
        readonly ILogger<SendEmailForSecondaryUnAllocationFailedBusGammaSubscriber> _logger;
        readonly ISendEmailForSecondaryUnAllocationFailed _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendEmailForSecondaryUnAllocationFailedBusGammaSubscriber(ILogger<SendEmailForSecondaryUnAllocationFailedBusGammaSubscriber> logger, ISendEmailForSecondaryUnAllocationFailed subscriber)
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
        public override async Task Handle(SecondaryUnAllocationFailedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
