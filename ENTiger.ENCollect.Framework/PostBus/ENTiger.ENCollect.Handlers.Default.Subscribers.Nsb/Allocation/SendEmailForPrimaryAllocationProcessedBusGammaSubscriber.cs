using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendEmailForPrimaryAllocationProcessedBusGammaSubscriber : NsbSubscriberBridge<PrimaryAllocationProcessedEvent>
    {
        readonly ILogger<SendEmailForPrimaryAllocationProcessedBusGammaSubscriber> _logger;
        readonly ISendEmailForPrimaryAllocationProcessed _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendEmailForPrimaryAllocationProcessedBusGammaSubscriber(ILogger<SendEmailForPrimaryAllocationProcessedBusGammaSubscriber> logger, ISendEmailForPrimaryAllocationProcessed subscriber)
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
        public override async Task Handle(PrimaryAllocationProcessedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
