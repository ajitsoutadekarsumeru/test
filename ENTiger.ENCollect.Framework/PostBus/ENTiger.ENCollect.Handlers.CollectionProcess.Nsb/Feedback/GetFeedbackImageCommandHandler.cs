using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.FeedbackModule
{
    /// <summary>
    /// 
    /// </summary>
    public class GetFeedbackImageCommandHandler : NsbCommandHandler<GetFeedbackImageCommand>
    {
        readonly ILogger<GetFeedbackImageCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public GetFeedbackImageCommandHandler(ILogger<GetFeedbackImageCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(GetFeedbackImageCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing GetFeedbackImageCommandHandler: {nameof(GetFeedbackImageCommandHandler)}");

            await this.ProcessHandlerSequence<GetFeedbackImagePostBusDataPacket, GetFeedbackImagePostBusSequence, 
                GetFeedbackImageCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
