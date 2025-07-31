using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    /// 
    /// </summary>
    public class AgencyUploadCommandHandler : NsbCommandHandler<AgencyUploadCommand>
    {
        readonly ILogger<AgencyUploadCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public AgencyUploadCommandHandler(ILogger<AgencyUploadCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(AgencyUploadCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing AgencyUploadCommandHandler: {nameof(AgencyUploadCommandHandler)}");

            await this.ProcessHandlerSequence<AgencyUploadPostBusDataPacket, AgencyUploadPostBusSequence, 
                AgencyUploadCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
