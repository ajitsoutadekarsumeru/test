using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateTemplateCommandHandler : NsbCommandHandler<UpdateTemplateCommand>
    {
        readonly ILogger<UpdateTemplateCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateTemplateCommandHandler(ILogger<UpdateTemplateCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(UpdateTemplateCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing UpdateTemplateCommandHandler: {nameof(UpdateTemplateCommandHandler)}");

            await this.ProcessHandlerSequence<UpdateTemplatePostBusDataPacket, UpdateTemplatePostBusSequence, 
                UpdateTemplateCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
