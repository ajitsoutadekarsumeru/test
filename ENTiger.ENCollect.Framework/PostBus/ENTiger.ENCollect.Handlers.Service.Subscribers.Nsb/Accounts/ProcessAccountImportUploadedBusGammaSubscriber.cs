using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class ProcessAccountImportUploadedBusGammaSubscriber : NsbSubscriberBridge<AccountImportUploadedEvent>
    {
        readonly ILogger<ProcessAccountImportUploadedBusGammaSubscriber> _logger;
        readonly IProcessAccountImportUploaded _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ProcessAccountImportUploadedBusGammaSubscriber(ILogger<ProcessAccountImportUploadedBusGammaSubscriber> logger, IProcessAccountImportUploaded subscriber)
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
        public override async Task Handle(AccountImportUploadedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
