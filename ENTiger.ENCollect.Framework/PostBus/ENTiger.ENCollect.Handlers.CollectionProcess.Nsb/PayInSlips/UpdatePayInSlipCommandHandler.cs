using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdatePayInSlipCommandHandler : NsbCommandHandler<UpdatePayInSlipCommand>
    {
        readonly ILogger<UpdatePayInSlipCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdatePayInSlipCommandHandler(ILogger<UpdatePayInSlipCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(UpdatePayInSlipCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing UpdatePayInSlipCommandHandler: {nameof(UpdatePayInSlipCommandHandler)}");

            await this.ProcessHandlerSequence<UpdatePayInSlipPostBusDataPacket, UpdatePayInSlipPostBusSequence, 
                UpdatePayInSlipCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
