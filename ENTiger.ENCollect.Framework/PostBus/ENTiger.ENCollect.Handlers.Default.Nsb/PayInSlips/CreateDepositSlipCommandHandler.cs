using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateDepositSlipCommandHandler : NsbCommandHandler<CreateDepositSlipCommand>
    {
        readonly ILogger<CreateDepositSlipCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public CreateDepositSlipCommandHandler(ILogger<CreateDepositSlipCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(CreateDepositSlipCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing CreateDepositSlipCommandHandler: {nameof(CreateDepositSlipCommandHandler)}");

            await this.ProcessHandlerSequence<CreateDepositSlipPostBusDataPacket, CreateDepositSlipPostBusSequence, 
                CreateDepositSlipCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
