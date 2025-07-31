using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendEmailForUpdateUsersFailedBusGammaSubscriber : NsbSubscriberBridge<UpdateUsersFailed>
    {
        readonly ILogger<SendEmailForUpdateUsersFailedBusGammaSubscriber> _logger;
        readonly ISendEmailForUpdateUsersFailed _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendEmailForUpdateUsersFailedBusGammaSubscriber(ILogger<SendEmailForUpdateUsersFailedBusGammaSubscriber> logger, ISendEmailForUpdateUsersFailed subscriber)
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
        public override async Task Handle(UpdateUsersFailed message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
