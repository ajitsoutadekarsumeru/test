using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.SettlementModule
{
    public partial class ProcessSettlementService : ProcessFlexServiceBridge
    {
        protected readonly IFlexServiceBusBridge _bus;
        protected readonly ILogger<ProcessSettlementService> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IFlexPrimaryKeyGeneratorBridge _pkGenerator;
        protected readonly ICustomUtility _customUtility;
        public ProcessSettlementService(IFlexServiceBusBridge bus,
            ILogger<ProcessSettlementService> logger,
            IFlexHost flexHost,
            IFlexPrimaryKeyGeneratorBridge pkGenerator,
            ICustomUtility customUtility)
        {
            _bus = bus;
            _logger = logger;
            _flexHost = flexHost;
            _pkGenerator = pkGenerator;
            _customUtility = customUtility;
        }
    }
}
