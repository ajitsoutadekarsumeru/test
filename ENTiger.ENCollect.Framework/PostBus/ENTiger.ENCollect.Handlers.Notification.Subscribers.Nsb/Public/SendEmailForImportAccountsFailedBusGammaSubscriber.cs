using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.PublicModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendEmailForImportAccountsFailedBusGammaSubscriber : NsbSubscriberBridge<ImportAccountsFailedEvent>
    {
        readonly ILogger<SendEmailForImportAccountsFailedBusGammaSubscriber> _logger;
        readonly ISendEmailForImportAccountsFailed _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendEmailForImportAccountsFailedBusGammaSubscriber(ILogger<SendEmailForImportAccountsFailedBusGammaSubscriber> logger, ISendEmailForImportAccountsFailed subscriber)
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
        public override async Task Handle(ImportAccountsFailedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
