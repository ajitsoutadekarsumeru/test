using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.DevicesModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SendOTPviaEmailForDeviceRegisterBusGammaSubscriber : NsbSubscriberBridge<DeviceRegistered>
    {
        readonly ILogger<SendOTPviaEmailForDeviceRegisterBusGammaSubscriber> _logger;
        readonly ISendOTPviaEmailForDeviceRegister _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SendOTPviaEmailForDeviceRegisterBusGammaSubscriber(ILogger<SendOTPviaEmailForDeviceRegisterBusGammaSubscriber> logger, ISendOTPviaEmailForDeviceRegister subscriber)
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
