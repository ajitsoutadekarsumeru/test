using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UploadCommandHandler : NsbCommandHandler<UploadCommand>
    {
        readonly ILogger<UploadCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UploadCommandHandler(ILogger<UploadCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(UploadCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing UploadCommandHandler: {nameof(UploadCommandHandler)}");

            await this.ProcessHandlerSequence<UploadPostBusDataPacket, UploadPostBusSequence, 
                UploadCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
