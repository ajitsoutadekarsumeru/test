using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class AddTemplateCommandHandler : NsbCommandHandler<AddTemplateCommand>
    {
        readonly ILogger<AddTemplateCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public AddTemplateCommandHandler(ILogger<AddTemplateCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(AddTemplateCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing AddTemplateCommandHandler: {nameof(AddTemplateCommandHandler)}");

            await this.ProcessHandlerSequence<AddTemplatePostBusDataPacket, AddTemplatePostBusSequence, 
                AddTemplateCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
