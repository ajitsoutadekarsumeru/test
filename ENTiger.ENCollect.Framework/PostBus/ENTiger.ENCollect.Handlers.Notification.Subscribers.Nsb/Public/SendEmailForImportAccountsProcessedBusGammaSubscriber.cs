using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.PublicModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendEmailForImportAccountsProcessedBusGammaSubscriber : NsbSubscriberBridge<ImportAccountsProcessedEvent>
    {
        readonly ILogger<SendEmailForImportAccountsProcessedBusGammaSubscriber> _logger;
        readonly ISendEmailForImportAccountsProcessed _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendEmailForImportAccountsProcessedBusGammaSubscriber(ILogger<SendEmailForImportAccountsProcessedBusGammaSubscriber> logger, ISendEmailForImportAccountsProcessed subscriber)
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
        public override async Task Handle(ImportAccountsProcessedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
