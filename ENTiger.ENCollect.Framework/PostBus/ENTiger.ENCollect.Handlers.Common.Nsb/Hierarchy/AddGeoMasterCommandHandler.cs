using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.HierarchyModule
{
    public class AddGeoMasterCommandHandler : NsbCommandHandler<AddGeoMasterCommand>
    {
        readonly ILogger<AddGeoMasterCommandHandler> _logger;
        readonly IFlexHost _flexHost;

        public AddGeoMasterCommandHandler(ILogger<AddGeoMasterCommandHandler> logger, IFlexHost flexHost)
        {
            _logger = logger;
            _flexHost = flexHost;
        }

        public override async Task Handle(AddGeoMasterCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing AddGeoMasterCommandHandler: {nameof(AddGeoMasterCommandHandler)}");

            await this.ProcessHandlerSequence<AddGeoMasterPostBusDataPacket, AddGeoMasterPostBusSequence,
                AddGeoMasterCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
