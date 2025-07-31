using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendEmailForAccountImportUploadedBusGammaSubscriber : NsbSubscriberBridge<AccountImportUploadedEvent>
    {
        readonly ILogger<SendEmailForAccountImportUploadedBusGammaSubscriber> _logger;
        readonly ISendEmailForAccountImportUploaded _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendEmailForAccountImportUploadedBusGammaSubscriber(ILogger<SendEmailForAccountImportUploadedBusGammaSubscriber> logger, ISendEmailForAccountImportUploaded subscriber)
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
