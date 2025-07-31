using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.DevicesModule
{
    /// <summary>
    /// 
    /// </summary>
    public class VerifyRegisteredDeviceCommandHandler : NsbCommandHandler<VerifyRegisteredDeviceCommand>
    {
        readonly ILogger<VerifyRegisteredDeviceCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public VerifyRegisteredDeviceCommandHandler(ILogger<VerifyRegisteredDeviceCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(VerifyRegisteredDeviceCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing VerifyRegisteredDeviceCommandHandler: {nameof(VerifyRegisteredDeviceCommandHandler)}");

            await this.ProcessHandlerSequence<VerifyRegisteredDevicePostBusDataPacket, VerifyRegisteredDevicePostBusSequence, 
                VerifyRegisteredDeviceCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
