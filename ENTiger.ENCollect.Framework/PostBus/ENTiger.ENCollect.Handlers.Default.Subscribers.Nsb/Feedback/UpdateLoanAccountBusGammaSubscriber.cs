using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.FeedbackModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateLoanAccountBusGammaSubscriber : NsbSubscriberBridge<FeedbackAddedEvent>
    {
        readonly ILogger<UpdateLoanAccountBusGammaSubscriber> _logger;
        readonly IUpdateLoanAccount _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateLoanAccountBusGammaSubscriber(ILogger<UpdateLoanAccountBusGammaSubscriber> logger, IUpdateLoanAccount subscriber)
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
        public override async Task Handle(FeedbackAddedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
