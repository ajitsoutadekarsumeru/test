using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateLoanAccountFlagCommandHandler : NsbCommandHandler<UpdateLoanAccountFlagCommand>
    {
        readonly ILogger<UpdateLoanAccountFlagCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateLoanAccountFlagCommandHandler(ILogger<UpdateLoanAccountFlagCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(UpdateLoanAccountFlagCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing UpdateLoanAccountFlagCommandHandler: {nameof(UpdateLoanAccountFlagCommandHandler)}");

            await this.ProcessHandlerSequence<UpdateLoanAccountFlagPostBusDataPacket, UpdateLoanAccountFlagPostBusSequence, 
                UpdateLoanAccountFlagCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
