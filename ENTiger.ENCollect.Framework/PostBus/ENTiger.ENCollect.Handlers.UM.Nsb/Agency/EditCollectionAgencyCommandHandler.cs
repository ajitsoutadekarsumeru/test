using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    /// 
    /// </summary>
    public class EditCollectionAgencyCommandHandler : NsbCommandHandler<EditCollectionAgencyCommand>
    {
        readonly ILogger<EditCollectionAgencyCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public EditCollectionAgencyCommandHandler(ILogger<EditCollectionAgencyCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(EditCollectionAgencyCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing EditCollectionAgencyCommandHandler: {nameof(EditCollectionAgencyCommandHandler)}");

            await this.ProcessHandlerSequence<EditCollectionAgencyPostBusDataPacket, EditCollectionAgencyPostBusSequence, 
                EditCollectionAgencyCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
