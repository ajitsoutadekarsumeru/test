using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendEmailForPrimaryUnAllocationProcessedBusGammaSubscriber : NsbSubscriberBridge<PrimaryUnAllocationProcessedEvent>
    {
        readonly ILogger<SendEmailForPrimaryUnAllocationProcessedBusGammaSubscriber> _logger;
        readonly ISendEmailForPrimaryUnAllocationProcessed _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendEmailForPrimaryUnAllocationProcessedBusGammaSubscriber(ILogger<SendEmailForPrimaryUnAllocationProcessedBusGammaSubscriber> logger, ISendEmailForPrimaryUnAllocationProcessed subscriber)
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
        public override async Task Handle(PrimaryUnAllocationProcessedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
