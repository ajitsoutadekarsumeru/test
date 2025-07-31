using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendEmailForAccountImportProcessedBusGammaSubscriber : NsbSubscriberBridge<AccountImportProcessedEvent>
    {
        readonly ILogger<SendEmailForAccountImportProcessedBusGammaSubscriber> _logger;
        readonly ISendEmailForAccountImportProcessed _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendEmailForAccountImportProcessedBusGammaSubscriber(ILogger<SendEmailForAccountImportProcessedBusGammaSubscriber> logger, ISendEmailForAccountImportProcessed subscriber)
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
        public override async Task Handle(AccountImportProcessedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
