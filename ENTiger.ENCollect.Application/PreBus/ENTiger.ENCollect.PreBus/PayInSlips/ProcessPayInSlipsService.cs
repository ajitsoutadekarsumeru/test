using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    public partial class ProcessPayInSlipsService : ProcessFlexServiceBridge
    {
        protected readonly IFlexServiceBusBridge _bus;
        protected readonly ILogger<ProcessPayInSlipsService> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IFlexPrimaryKeyGeneratorBridge _pkGenerator;
        protected readonly ICustomUtility _customUtility;
        public ProcessPayInSlipsService(IFlexServiceBusBridge bus, ILogger<ProcessPayInSlipsService> logger, IFlexHost flexHost, IFlexPrimaryKeyGeneratorBridge pkGenerator, ICustomUtility customUtility)
        {
            _bus = bus;
            _logger = logger;
            _flexHost = flexHost;
            _pkGenerator = pkGenerator;
            _customUtility = customUtility;
        }
    }
}