using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendEmailForMastersImportFailedBusGammaSubscriber : NsbSubscriberBridge<MastersImportFailedEvent>
    {
        readonly ILogger<SendEmailForMastersImportFailedBusGammaSubscriber> _logger;
        readonly ISendEmailForMastersImportFailed _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendEmailForMastersImportFailedBusGammaSubscriber(ILogger<SendEmailForMastersImportFailedBusGammaSubscriber> logger, ISendEmailForMastersImportFailed subscriber)
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
        public override async Task Handle(MastersImportFailedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
