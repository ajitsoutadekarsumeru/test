using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AuditTrailModule
{
    public partial class ProcessAuditTrailService : ProcessFlexServiceBridge
    {
        protected readonly IFlexServiceBusBridge _bus;
        protected readonly ILogger<ProcessAuditTrailService> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IFlexPrimaryKeyGeneratorBridge _pkGenerator;

        public ProcessAuditTrailService(IFlexServiceBusBridge bus, ILogger<ProcessAuditTrailService> logger, IFlexHost flexHost, IFlexPrimaryKeyGeneratorBridge pkGenerator)
        {
            _bus = bus;
            _logger = logger;
            _flexHost = flexHost;
            _pkGenerator = pkGenerator;
        }
    }
}
