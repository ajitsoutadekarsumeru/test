using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    /// 
    /// </summary>
    public class ProcessMastersImportUploadedBusGammaSubscriber : NsbSubscriberBridge<MastersImportUploadedEvent>
    {
        readonly ILogger<ProcessMastersImportUploadedBusGammaSubscriber> _logger;
        readonly IProcessMastersImportUploaded _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ProcessMastersImportUploadedBusGammaSubscriber(ILogger<ProcessMastersImportUploadedBusGammaSubscriber> logger, IProcessMastersImportUploaded subscriber)
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
        public override async Task Handle(MastersImportUploadedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
