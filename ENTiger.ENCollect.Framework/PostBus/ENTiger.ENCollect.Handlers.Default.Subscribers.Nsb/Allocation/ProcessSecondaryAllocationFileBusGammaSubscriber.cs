using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class ProcessSecondaryAllocationFileBusGammaSubscriber : NsbSubscriberBridge<SecondaryAllocationUploadedEvent>
    {
        readonly ILogger<ProcessSecondaryAllocationFileBusGammaSubscriber> _logger;
        readonly IProcessSecondaryAllocationFile _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ProcessSecondaryAllocationFileBusGammaSubscriber(ILogger<ProcessSecondaryAllocationFileBusGammaSubscriber> logger, IProcessSecondaryAllocationFile subscriber)
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
        public override async Task Handle(SecondaryAllocationUploadedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
