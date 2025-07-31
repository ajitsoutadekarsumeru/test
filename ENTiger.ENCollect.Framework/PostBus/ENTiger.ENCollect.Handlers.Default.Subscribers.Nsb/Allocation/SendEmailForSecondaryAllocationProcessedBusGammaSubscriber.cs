using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendEmailForSecondaryAllocationProcessedBusGammaSubscriber : NsbSubscriberBridge<SecondaryAllocationProcessedEvent>
    {
        readonly ILogger<SendEmailForSecondaryAllocationProcessedBusGammaSubscriber> _logger;
        readonly ISendEmailForSecondaryAllocationProcessed _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendEmailForSecondaryAllocationProcessedBusGammaSubscriber(ILogger<SendEmailForSecondaryAllocationProcessedBusGammaSubscriber> logger, ISendEmailForSecondaryAllocationProcessed subscriber)
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
        public override async Task Handle(SecondaryAllocationProcessedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
