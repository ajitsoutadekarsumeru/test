using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    /// 
    /// </summary>
    public class AddAgencyCommandHandler : NsbCommandHandler<AddAgencyCommand>
    {
        readonly ILogger<AddAgencyCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public AddAgencyCommandHandler(ILogger<AddAgencyCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(AddAgencyCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing AddAgencyCommandHandler: {nameof(AddAgencyCommandHandler)}");

            await this.ProcessHandlerSequence<AddAgencyPostBusDataPacket, AddAgencyPostBusSequence, 
                AddAgencyCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
