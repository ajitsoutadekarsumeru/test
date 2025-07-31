using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.FeedbackModule
{
    /// <summary>
    /// 
    /// </summary>
    public class GetTrailFileCommandHandler : NsbCommandHandler<GetTrailFileCommand>
    {
        readonly ILogger<GetTrailFileCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public GetTrailFileCommandHandler(ILogger<GetTrailFileCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(GetTrailFileCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing GetTrailFileCommandHandler: {nameof(GetTrailFileCommandHandler)}");

            await this.ProcessHandlerSequence<GetTrailFilePostBusDataPacket, GetTrailFilePostBusSequence, 
                GetTrailFileCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
