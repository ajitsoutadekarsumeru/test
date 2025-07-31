using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class ProcessSecondaryUnAllocationUploadedBusGammaSubscriber : NsbSubscriberBridge<SecondaryUnAllocationUploadedEvent>
    {
        readonly ILogger<ProcessSecondaryUnAllocationUploadedBusGammaSubscriber> _logger;
        readonly IProcessSecondaryUnAllocationUploaded _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ProcessSecondaryUnAllocationUploadedBusGammaSubscriber(ILogger<ProcessSecondaryUnAllocationUploadedBusGammaSubscriber> logger, IProcessSecondaryUnAllocationUploaded subscriber)
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
