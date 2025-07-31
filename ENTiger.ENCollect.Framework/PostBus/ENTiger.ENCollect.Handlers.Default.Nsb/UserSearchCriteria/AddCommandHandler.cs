using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.UserSearchCriteriaModule
{
    /// <summary>
    /// 
    /// </summary>
    public class AddCommandHandler : NsbCommandHandler<AddCommand>
    {
        readonly ILogger<AddCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public AddCommandHandler(ILogger<AddCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(AddCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing AddCommandHandler: {nameof(AddCommandHandler)}");

            await this.ProcessHandlerSequence<AddPostBusDataPacket, AddPostBusSequence, 
                AddCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
