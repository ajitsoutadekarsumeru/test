using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.DevicesModule
{
    /// <summary>
    /// 
    /// </summary>
    public class RegisterDeviceCommandHandler : NsbCommandHandler<RegisterDeviceCommand>
    {
        readonly ILogger<RegisterDeviceCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public RegisterDeviceCommandHandler(ILogger<RegisterDeviceCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(RegisterDeviceCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing RegisterDeviceCommandHandler: {nameof(RegisterDeviceCommandHandler)}");

            await this.ProcessHandlerSequence<RegisterDevicePostBusDataPacket, RegisterDevicePostBusSequence, 
                RegisterDeviceCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
