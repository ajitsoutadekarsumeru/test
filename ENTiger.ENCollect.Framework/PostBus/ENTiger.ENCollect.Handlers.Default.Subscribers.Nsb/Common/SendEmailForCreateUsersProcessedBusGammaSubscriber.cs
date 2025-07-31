using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendEmailForCreateUsersProcessedBusGammaSubscriber : NsbSubscriberBridge<CreateUsersProcessed>
    {
        readonly ILogger<SendEmailForCreateUsersProcessedBusGammaSubscriber> _logger;
        readonly ISendEmailForCreateUsersProcessed _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendEmailForCreateUsersProcessedBusGammaSubscriber(ILogger<SendEmailForCreateUsersProcessedBusGammaSubscriber> logger, ISendEmailForCreateUsersProcessed subscriber)
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
        public override async Task Handle(CreateUsersProcessed message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
