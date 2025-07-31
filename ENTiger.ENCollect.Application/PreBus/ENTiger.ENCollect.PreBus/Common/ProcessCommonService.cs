using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class ProcessCommonService : ProcessFlexServiceBridge
    {
        protected readonly IFlexServiceBusBridge _bus;
        protected readonly ILogger<ProcessCommonService> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IFlexPrimaryKeyGeneratorBridge _pkGenerator;
        protected readonly IRepoFactory _repoFactory;

        public ProcessCommonService(IFlexServiceBusBridge bus, ILogger<ProcessCommonService> logger, IFlexHost flexHost, IFlexPrimaryKeyGeneratorBridge pkGenerator, IRepoFactory repoFactory)
        {
            _bus = bus;
            _logger = logger;
            _flexHost = flexHost;
            _pkGenerator = pkGenerator;
            _repoFactory = repoFactory;
        }
    }
}