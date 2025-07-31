using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteTemplateCommandHandler : NsbCommandHandler<DeleteTemplateCommand>
    {
        readonly ILogger<DeleteTemplateCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public DeleteTemplateCommandHandler(ILogger<DeleteTemplateCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(DeleteTemplateCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing DeleteTemplateCommandHandler: {nameof(DeleteTemplateCommandHandler)}");

            await this.ProcessHandlerSequence<DeleteTemplatePostBusDataPacket, DeleteTemplatePostBusSequence, 
                DeleteTemplateCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
