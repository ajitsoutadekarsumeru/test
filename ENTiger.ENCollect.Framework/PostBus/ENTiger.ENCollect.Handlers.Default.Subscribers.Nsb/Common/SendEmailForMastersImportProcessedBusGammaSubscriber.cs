using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendEmailForMastersImportProcessedBusGammaSubscriber : NsbSubscriberBridge<MastersImportProcessedEvent>
    {
        readonly ILogger<SendEmailForMastersImportProcessedBusGammaSubscriber> _logger;
        readonly ISendEmailForMastersImportProcessed _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendEmailForMastersImportProcessedBusGammaSubscriber(ILogger<SendEmailForMastersImportProcessedBusGammaSubscriber> logger, ISendEmailForMastersImportProcessed subscriber)
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
        public override async Task Handle(MastersImportProcessedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
