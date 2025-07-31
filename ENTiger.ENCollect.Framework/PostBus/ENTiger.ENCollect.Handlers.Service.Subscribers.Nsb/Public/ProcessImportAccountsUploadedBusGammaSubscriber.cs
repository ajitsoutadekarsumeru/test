using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.PublicModule
{
    /// <summary>
    /// 
    /// </summary>
    public class ProcessImportAccountsUploadedBusGammaSubscriber : NsbSubscriberBridge<ImportAccountsUploadedEvent>
    {
        readonly ILogger<ProcessImportAccountsUploadedBusGammaSubscriber> _logger;
        readonly IProcessImportAccountsUploaded _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ProcessImportAccountsUploadedBusGammaSubscriber(ILogger<ProcessImportAccountsUploadedBusGammaSubscriber> logger, IProcessImportAccountsUploaded subscriber)
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
        public override async Task Handle(ImportAccountsUploadedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
