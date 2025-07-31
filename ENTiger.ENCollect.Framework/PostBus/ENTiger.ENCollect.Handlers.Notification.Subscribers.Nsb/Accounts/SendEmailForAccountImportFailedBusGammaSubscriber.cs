using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendEmailForAccountImportFailedBusGammaSubscriber : NsbSubscriberBridge<AccountImportFailedEvent>
    {
        readonly ILogger<SendEmailForAccountImportFailedBusGammaSubscriber> _logger;
        readonly ISendEmailForAccountImportFailed _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendEmailForAccountImportFailedBusGammaSubscriber(ILogger<SendEmailForAccountImportFailedBusGammaSubscriber> logger, ISendEmailForAccountImportFailed subscriber)
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
        public override async Task Handle(AccountImportFailedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
