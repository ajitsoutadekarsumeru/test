using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.ReportsModule
{
    public partial class ProcessReportsService : ProcessFlexServiceBridge
    {
        protected readonly IFlexServiceBusBridge _bus;
        protected readonly ILogger<ProcessReportsService> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IFlexPrimaryKeyGeneratorBridge _pkGenerator;

        public ProcessReportsService(IFlexServiceBusBridge bus, ILogger<ProcessReportsService> logger, IFlexHost flexHost, IFlexPrimaryKeyGeneratorBridge pkGenerator)
        {
            _bus = bus;
            _logger = logger;
            _flexHost = flexHost;
            _pkGenerator = pkGenerator;
        }
    }
}
