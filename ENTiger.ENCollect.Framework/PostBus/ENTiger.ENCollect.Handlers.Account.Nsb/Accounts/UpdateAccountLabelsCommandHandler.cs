using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateAccountLabelsCommandHandler : NsbCommandHandler<UpdateAccountLabelsCommand>
    {
        readonly ILogger<UpdateAccountLabelsCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateAccountLabelsCommandHandler(ILogger<UpdateAccountLabelsCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(UpdateAccountLabelsCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing UpdateAccountLabelsCommandHandler: {nameof(UpdateAccountLabelsCommandHandler)}");

            await this.ProcessHandlerSequence<UpdateAccountLabelsPostBusDataPacket, UpdateAccountLabelsPostBusSequence, 
                UpdateAccountLabelsCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
