using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.UserSearchCriteriaModule
{
    public partial class ProcessUserSearchCriteriaService : ProcessFlexServiceBridge
    {
        protected readonly IFlexServiceBusBridge _bus;
        protected readonly ILogger<ProcessUserSearchCriteriaService> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IFlexPrimaryKeyGeneratorBridge _pkGenerator;

        public ProcessUserSearchCriteriaService(IFlexServiceBusBridge bus, ILogger<ProcessUserSearchCriteriaService> logger, IFlexHost flexHost, IFlexPrimaryKeyGeneratorBridge pkGenerator)
        {
            _bus = bus;
            _logger = logger;
            _flexHost = flexHost;
            _pkGenerator = pkGenerator;
        }
    }
}