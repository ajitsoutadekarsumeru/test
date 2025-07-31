using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.PublicModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendEmailForImportAccountsUploadedBusGammaSubscriber : NsbSubscriberBridge<ImportAccountsUploadedEvent>
    {
        readonly ILogger<SendEmailForImportAccountsUploadedBusGammaSubscriber> _logger;
        readonly ISendEmailForImportAccountsUploaded _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendEmailForImportAccountsUploadedBusGammaSubscriber(ILogger<SendEmailForImportAccountsUploadedBusGammaSubscriber> logger, ISendEmailForImportAccountsUploaded subscriber)
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
