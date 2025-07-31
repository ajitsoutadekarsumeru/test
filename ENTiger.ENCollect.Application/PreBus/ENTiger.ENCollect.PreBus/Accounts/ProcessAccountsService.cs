using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class ProcessAccountsService : ProcessFlexServiceBridge
    {
        protected readonly IFlexServiceBusBridge _bus;
        protected readonly ILogger<ProcessAccountsService> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IFlexPrimaryKeyGeneratorBridge _pkGenerator;
        protected readonly IRepoFactory _repoFactory;

        public ProcessAccountsService(IFlexServiceBusBridge bus, ILogger<ProcessAccountsService> logger, IFlexHost flexHost, IFlexPrimaryKeyGeneratorBridge pkGenerator, IRepoFactory repoFactory)
        {
            _bus = bus;
            _logger = logger;
            _flexHost = flexHost;
            _pkGenerator = pkGenerator;
            _repoFactory = repoFactory;
        }
    }
}