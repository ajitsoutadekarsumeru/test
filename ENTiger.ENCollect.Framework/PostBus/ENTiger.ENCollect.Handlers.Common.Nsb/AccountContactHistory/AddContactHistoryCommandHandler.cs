using ENTiger.ENCollect.AccountContactHistoryModule;
using ENTiger.ENCollect.Messages.Commands.ContactHistory;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;


namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    /// 
    /// </summary>
    public class AddContactHistoryCommandHandler : NsbCommandHandler<AddContactHistoryCommand>
    {
        readonly ILogger<AddContactHistoryCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public AddContactHistoryCommandHandler(ILogger<AddContactHistoryCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(AddContactHistoryCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing AddContactHistoryCommandHandler: {nameof(AddContactHistoryCommandHandler)}");

            await this.ProcessHandlerSequence<AddContactHistoryPostBusDataPacket, AddContactHistoryPostBusSequence,
                AddContactHistoryCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
