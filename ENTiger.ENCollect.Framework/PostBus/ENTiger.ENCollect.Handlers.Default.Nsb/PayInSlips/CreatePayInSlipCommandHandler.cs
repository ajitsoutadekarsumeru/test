using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class CreatePayInSlipCommandHandler : NsbCommandHandler<CreatePayInSlipCommand>
    {
        readonly ILogger<CreatePayInSlipCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public CreatePayInSlipCommandHandler(ILogger<CreatePayInSlipCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(CreatePayInSlipCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing CreatePayInSlipCommandHandler: {nameof(CreatePayInSlipCommandHandler)}");

            await this.ProcessHandlerSequence<CreatePayInSlipPostBusDataPacket, CreatePayInSlipPostBusSequence, 
                CreatePayInSlipCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
