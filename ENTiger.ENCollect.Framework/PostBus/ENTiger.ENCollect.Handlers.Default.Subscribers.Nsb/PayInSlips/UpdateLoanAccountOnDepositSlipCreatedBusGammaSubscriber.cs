using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateLoanAccountOnDepositSlipCreatedBusGammaSubscriber : NsbSubscriberBridge<PayInSlipCreatedEvent>
    {
        readonly ILogger<UpdateLoanAccountOnDepositSlipCreatedBusGammaSubscriber> _logger;
        readonly IUpdateLoanAccountOnDepositSlipCreated _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateLoanAccountOnDepositSlipCreatedBusGammaSubscriber(ILogger<UpdateLoanAccountOnDepositSlipCreatedBusGammaSubscriber> logger, IUpdateLoanAccountOnDepositSlipCreated subscriber)
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
        public override async Task Handle(PayInSlipCreatedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
