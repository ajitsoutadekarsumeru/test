using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.PublicModule
{
    /// <summary>
    /// 
    /// </summary>
    public class ImportCollectionsCommandHandler : NsbCommandHandler<ImportCollectionsCommand>
    {
        readonly ILogger<ImportCollectionsCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ImportCollectionsCommandHandler(ILogger<ImportCollectionsCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(ImportCollectionsCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing ImportCollectionsCommandHandler: {nameof(ImportCollectionsCommandHandler)}");

            await this.ProcessHandlerSequence<ImportCollectionsPostBusDataPacket, ImportCollectionsPostBusSequence, 
                ImportCollectionsCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
