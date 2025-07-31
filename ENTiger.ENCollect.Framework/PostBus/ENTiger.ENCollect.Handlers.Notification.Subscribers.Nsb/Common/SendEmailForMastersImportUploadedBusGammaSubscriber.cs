using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendEmailForMastersImportUploadedBusGammaSubscriber : NsbSubscriberBridge<MastersImportUploadedEvent>
    {
        readonly ILogger<SendEmailForMastersImportUploadedBusGammaSubscriber> _logger;
        readonly ISendEmailForMastersImportUploaded _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendEmailForMastersImportUploadedBusGammaSubscriber(ILogger<SendEmailForMastersImportUploadedBusGammaSubscriber> logger, ISendEmailForMastersImportUploaded subscriber)
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
        public override async Task Handle(MastersImportUploadedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
