using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendEmailForSecondaryUnAllocationProcessedBusGammaSubscriber : NsbSubscriberBridge<SecondaryUnAllocationProcessedEvent>
    {
        readonly ILogger<SendEmailForSecondaryUnAllocationProcessedBusGammaSubscriber> _logger;
        readonly ISendEmailForSecondaryUnAllocationProcessed _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendEmailForSecondaryUnAllocationProcessedBusGammaSubscriber(ILogger<SendEmailForSecondaryUnAllocationProcessedBusGammaSubscriber> logger, ISendEmailForSecondaryUnAllocationProcessed subscriber)
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
        public override async Task Handle(SecondaryUnAllocationProcessedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
