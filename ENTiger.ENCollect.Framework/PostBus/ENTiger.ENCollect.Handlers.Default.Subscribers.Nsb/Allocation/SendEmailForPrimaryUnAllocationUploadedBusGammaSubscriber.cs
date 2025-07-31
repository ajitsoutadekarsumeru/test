using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendEmailForPrimaryUnAllocationUploadedBusGammaSubscriber : NsbSubscriberBridge<PrimaryUnAllocationUploadedEvent>
    {
        readonly ILogger<SendEmailForPrimaryUnAllocationUploadedBusGammaSubscriber> _logger;
        readonly ISendEmailForPrimaryUnAllocationUploaded _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendEmailForPrimaryUnAllocationUploadedBusGammaSubscriber(ILogger<SendEmailForPrimaryUnAllocationUploadedBusGammaSubscriber> logger, ISendEmailForPrimaryUnAllocationUploaded subscriber)
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
        public override async Task Handle(PrimaryUnAllocationUploadedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
