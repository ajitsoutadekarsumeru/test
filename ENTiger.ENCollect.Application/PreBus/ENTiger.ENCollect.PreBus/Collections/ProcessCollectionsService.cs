using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class ProcessCollectionsService : ProcessFlexServiceBridge
    {
        protected readonly IFlexServiceBusBridge _bus;
        protected readonly ILogger<ProcessCollectionsService> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IFlexPrimaryKeyGeneratorBridge _pkGenerator;
        protected readonly ICustomUtility _customUtility;
        public ProcessCollectionsService(IFlexServiceBusBridge bus, ILogger<ProcessCollectionsService> logger, IFlexHost flexHost, IFlexPrimaryKeyGeneratorBridge pkGenerator, ICustomUtility customUtility)
        {
            _bus = bus;
            _logger = logger;
            _flexHost = flexHost;
            _pkGenerator = pkGenerator;
            _customUtility = customUtility;
        }
    }
}