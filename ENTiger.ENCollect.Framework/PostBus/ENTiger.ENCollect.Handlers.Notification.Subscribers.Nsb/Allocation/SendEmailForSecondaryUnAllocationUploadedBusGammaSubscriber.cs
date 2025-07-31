using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendEmailForSecondaryUnAllocationUploadedBusGammaSubscriber : NsbSubscriberBridge<SecondaryUnAllocationUploadedEvent>
    {
        readonly ILogger<SendEmailForSecondaryUnAllocationUploadedBusGammaSubscriber> _logger;
        readonly ISendEmailForSecondaryUnAllocationUploaded _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendEmailForSecondaryUnAllocationUploadedBusGammaSubscriber(ILogger<SendEmailForSecondaryUnAllocationUploadedBusGammaSubscriber> logger, ISendEmailForSecondaryUnAllocationUploaded subscriber)
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
        public override async Task Handle(SecondaryUnAllocationUploadedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
