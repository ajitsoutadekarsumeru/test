using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;
using ENTiger.ENCollect.CollectionsModule;

namespace ENTiger.ENCollect.FeedbackModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateLoanAccountsProjectionOnAddFeedbackBusGammaSubscriber : NsbSubscriberBridge<FeedbackAddedEvent>
    {
        readonly ILogger<UpdateLoanAccountsProjectionOnAddFeedbackBusGammaSubscriber> _logger;
        readonly IUpdateLoanAccountsProjectionOnAddFeedback _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateLoanAccountsProjectionOnAddFeedbackBusGammaSubscriber(ILogger<UpdateLoanAccountsProjectionOnAddFeedbackBusGammaSubscriber> logger, IUpdateLoanAccountsProjectionOnAddFeedback subscriber)
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
