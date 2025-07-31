using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendEmailForPrimaryAllocationUploadedBusGammaSubscriber : NsbSubscriberBridge<PrimaryAllocationUploadedEvent>
    {
        readonly ILogger<SendEmailForPrimaryAllocationUploadedBusGammaSubscriber> _logger;
        readonly ISendEmailForPrimaryAllocationUploaded _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendEmailForPrimaryAllocationUploadedBusGammaSubscriber(ILogger<SendEmailForPrimaryAllocationUploadedBusGammaSubscriber> logger, ISendEmailForPrimaryAllocationUploaded subscriber)
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
        public override async Task Handle(PrimaryAllocationUploadedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
