using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class AddLoanAccountFlagCommandHandler : NsbCommandHandler<AddLoanAccountFlagCommand>
    {
        readonly ILogger<AddLoanAccountFlagCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public AddLoanAccountFlagCommandHandler(ILogger<AddLoanAccountFlagCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(AddLoanAccountFlagCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing AddLoanAccountFlagCommandHandler: {nameof(AddLoanAccountFlagCommandHandler)}");

            await this.ProcessHandlerSequence<AddLoanAccountFlagPostBusDataPacket, AddLoanAccountFlagPostBusSequence, 
                AddLoanAccountFlagCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
