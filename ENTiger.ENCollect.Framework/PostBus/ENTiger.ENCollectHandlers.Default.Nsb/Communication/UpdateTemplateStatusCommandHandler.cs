using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateTemplateStatusCommandHandler : NsbCommandHandler<UpdateTemplateStatusCommand>
    {
        readonly ILogger<UpdateTemplateStatusCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateTemplateStatusCommandHandler(ILogger<UpdateTemplateStatusCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(UpdateTemplateStatusCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing UpdateTemplateStatusCommandHandler: {nameof(UpdateTemplateStatusCommandHandler)}");

            await this.ProcessHandlerSequence<UpdateTemplateStatusPostBusDataPacket, UpdateTemplateStatusPostBusSequence, 
                UpdateTemplateStatusCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
