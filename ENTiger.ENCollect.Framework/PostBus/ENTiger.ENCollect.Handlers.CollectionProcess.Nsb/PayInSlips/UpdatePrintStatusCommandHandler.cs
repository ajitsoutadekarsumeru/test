using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdatePrintStatusCommandHandler : NsbCommandHandler<UpdatePrintStatusCommand>
    {
        readonly ILogger<UpdatePrintStatusCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdatePrintStatusCommandHandler(ILogger<UpdatePrintStatusCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(UpdatePrintStatusCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing UpdatePrintStatusCommandHandler: {nameof(UpdatePrintStatusCommandHandler)}");

            await this.ProcessHandlerSequence<UpdatePrintStatusPostBusDataPacket, UpdatePrintStatusPostBusSequence, 
                UpdatePrintStatusCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
