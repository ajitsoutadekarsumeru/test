using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendEmailForUpdateUsersProcessedBusGammaSubscriber : NsbSubscriberBridge<UpdateUsersProcessed>
    {
        readonly ILogger<SendEmailForUpdateUsersProcessedBusGammaSubscriber> _logger;
        readonly ISendEmailForUpdateUsersProcessed _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendEmailForUpdateUsersProcessedBusGammaSubscriber(ILogger<SendEmailForUpdateUsersProcessedBusGammaSubscriber> logger, ISendEmailForUpdateUsersProcessed subscriber)
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
        public override async Task Handle(UpdateUsersProcessed message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
