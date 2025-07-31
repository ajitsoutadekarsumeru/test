using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UploadFileCommandHandler : NsbCommandHandler<UploadFileCommand>
    {
        readonly ILogger<UploadFileCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UploadFileCommandHandler(ILogger<UploadFileCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(UploadFileCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing UploadFileCommandHandler: {nameof(UploadFileCommandHandler)}");

            await this.ProcessHandlerSequence<UploadFilePostBusDataPacket, UploadFilePostBusSequence, 
                UploadFileCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
