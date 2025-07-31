using ENTiger.ENCollect.ApplicationUsersModule;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    /// 
    /// </summary>
    public class ProcessSendEmailSMSForLicenseTransactionLimitBusGammaSubscriber : NsbSubscriberBridge<TransactionLicenseLimitExceeded>
    {
        readonly ILogger<ProcessSendEmailSMSForLicenseTransactionLimitBusGammaSubscriber> _logger;
        readonly ISendEmailForLicenseTransactionLimit _subscriber;
        private readonly ISendSMSForLicenseTransactionLimit _smsSubscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ProcessSendEmailSMSForLicenseTransactionLimitBusGammaSubscriber(ILogger<ProcessSendEmailSMSForLicenseTransactionLimitBusGammaSubscriber> logger, ISendEmailForLicenseTransactionLimit subscriber, ISendSMSForLicenseTransactionLimit smsSubscriber)
        {
            _logger = logger;
            _subscriber = subscriber;
            _smsSubscriber = smsSubscriber;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task Handle(TransactionLicenseLimitExceeded message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
            await _smsSubscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
