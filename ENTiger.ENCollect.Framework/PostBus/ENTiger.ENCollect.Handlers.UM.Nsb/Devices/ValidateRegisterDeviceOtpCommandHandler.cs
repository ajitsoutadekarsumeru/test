using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.DevicesModule
{
    /// <summary>
    /// 
    /// </summary>
    public class ValidateRegisterDeviceOtpCommandHandler : NsbCommandHandler<ValidateRegisterDeviceOtpCommand>
    {
        readonly ILogger<ValidateRegisterDeviceOtpCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ValidateRegisterDeviceOtpCommandHandler(ILogger<ValidateRegisterDeviceOtpCommandHandler> logger, IFlexHost flexHost)
        {
            _logger = logger;
            _flexHost = flexHost;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task Handle(ValidateRegisterDeviceOtpCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing ValidateRegisterDeviceOtpCommandHandler: {nameof(ValidateRegisterDeviceOtpCommandHandler)}");

            await this.ProcessHandlerSequence<ValidateRegisterDeviceOtpPostBusDataPacket, ValidateRegisterDeviceOtpPostBusSequence, 
                ValidateRegisterDeviceOtpCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
