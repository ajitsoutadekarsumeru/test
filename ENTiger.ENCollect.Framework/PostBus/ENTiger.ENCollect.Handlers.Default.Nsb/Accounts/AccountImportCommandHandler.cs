using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountImportCommandHandler : NsbCommandHandler<AccountImportCommand>
    {
        readonly ILogger<AccountImportCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public AccountImportCommandHandler(ILogger<AccountImportCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(AccountImportCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing AccountImportCommandHandler: {nameof(AccountImportCommandHandler)}");

            await this.ProcessHandlerSequence<AccountImportPostBusDataPacket, AccountImportPostBusSequence, 
                AccountImportCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
