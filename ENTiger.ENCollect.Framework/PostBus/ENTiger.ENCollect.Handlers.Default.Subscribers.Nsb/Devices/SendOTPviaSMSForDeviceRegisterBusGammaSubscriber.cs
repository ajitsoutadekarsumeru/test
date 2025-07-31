using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.DevicesModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendOTPviaSMSForDeviceRegisterBusGammaSubscriber : NsbSubscriberBridge<DeviceRegistered>
    {
        readonly ILogger<SendOTPviaSMSForDeviceRegisterBusGammaSubscriber> _logger;
        readonly ISendOTPviaSMSForDeviceRegister _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendOTPviaSMSForDeviceRegisterBusGammaSubscriber(ILogger<SendOTPviaSMSForDeviceRegisterBusGammaSubscriber> logger, ISendOTPviaSMSForDeviceRegister subscriber)
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
        public override async Task Handle(DeviceRegistered message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
