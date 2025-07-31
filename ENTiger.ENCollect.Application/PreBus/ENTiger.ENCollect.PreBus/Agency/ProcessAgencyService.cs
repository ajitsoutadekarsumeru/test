using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class ProcessAgencyService : ProcessFlexServiceBridge
    {
        protected readonly IFlexServiceBusBridge _bus;
        protected readonly ILogger<ProcessAgencyService> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IFlexPrimaryKeyGeneratorBridge _pkGenerator;

        public ProcessAgencyService(IFlexServiceBusBridge bus, ILogger<ProcessAgencyService> logger, IFlexHost flexHost, IFlexPrimaryKeyGeneratorBridge pkGenerator)
        {
            _bus = bus;
            _logger = logger;
            _flexHost = flexHost;
            _pkGenerator = pkGenerator;
        }
    }
}