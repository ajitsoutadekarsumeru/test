using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.PublicModule
{
    /// <summary>
    /// 
    /// </summary>
    public class ImportAccountsCommandHandler : NsbCommandHandler<ImportAccountsCommand>
    {
        readonly ILogger<ImportAccountsCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ImportAccountsCommandHandler(ILogger<ImportAccountsCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(ImportAccountsCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing ImportAccountsCommandHandler: {nameof(ImportAccountsCommandHandler)}");

            await this.ProcessHandlerSequence<ImportAccountsPostBusDataPacket, ImportAccountsPostBusSequence, 
                ImportAccountsCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
