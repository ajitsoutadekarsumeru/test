using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendEmailForUpdateUsersUploadedBusGammaSubscriber : NsbSubscriberBridge<UpdateUsersUploadedEvent>
    {
        readonly ILogger<SendEmailForUpdateUsersUploadedBusGammaSubscriber> _logger;
        readonly ISendEmailForUpdateUsersUploaded _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendEmailForUpdateUsersUploadedBusGammaSubscriber(ILogger<SendEmailForUpdateUsersUploadedBusGammaSubscriber> logger, ISendEmailForUpdateUsersUploaded subscriber)
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
        public override async Task Handle(UpdateUsersUploadedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
