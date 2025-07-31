using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.FeedbackModule
{
    /// <summary>
    /// 
    /// </summary>
    public class AddFeedbackCommandHandler : NsbCommandHandler<AddFeedbackCommand>
    {
        readonly ILogger<AddFeedbackCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public AddFeedbackCommandHandler(ILogger<AddFeedbackCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(AddFeedbackCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing AddFeedbackCommandHandler: {nameof(AddFeedbackCommandHandler)}");

            await this.ProcessHandlerSequence<AddFeedbackPostBusDataPacket, AddFeedbackPostBusSequence, 
                AddFeedbackCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
