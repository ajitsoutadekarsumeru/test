using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendEmailForCreateUsersFailedBusGammaSubscriber : NsbSubscriberBridge<CreateUsersFailed>
    {
        readonly ILogger<SendEmailForCreateUsersFailedBusGammaSubscriber> _logger;
        readonly ISendEmailForCreateUsersFailed _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendEmailForCreateUsersFailedBusGammaSubscriber(ILogger<SendEmailForCreateUsersFailedBusGammaSubscriber> logger, ISendEmailForCreateUsersFailed subscriber)
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
        public override async Task Handle(CreateUsersFailed message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
