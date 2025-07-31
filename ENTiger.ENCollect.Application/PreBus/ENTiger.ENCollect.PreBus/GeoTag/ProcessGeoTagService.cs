using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.GeoTagModule
{
    public partial class ProcessGeoTagService : ProcessFlexServiceBridge
    {
        protected readonly IFlexServiceBusBridge _bus;
        protected readonly ILogger<ProcessGeoTagService> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IFlexPrimaryKeyGeneratorBridge _pkGenerator;

        public ProcessGeoTagService(IFlexServiceBusBridge bus, ILogger<ProcessGeoTagService> logger, IFlexHost flexHost, IFlexPrimaryKeyGeneratorBridge pkGenerator)
        {
            _bus = bus;
            _logger = logger;
            _flexHost = flexHost;
            _pkGenerator = pkGenerator;
        }
    }
}