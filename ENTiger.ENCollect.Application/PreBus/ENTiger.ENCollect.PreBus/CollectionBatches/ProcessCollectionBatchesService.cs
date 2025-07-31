using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionBatchesModule
{
    public partial class ProcessCollectionBatchesService : ProcessFlexServiceBridge
    {
        protected readonly IFlexServiceBusBridge _bus;
        protected readonly ILogger<ProcessCollectionBatchesService> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IFlexPrimaryKeyGeneratorBridge _pkGenerator;
        protected readonly ICustomUtility _customUtility;
        public ProcessCollectionBatchesService(IFlexServiceBusBridge bus, ILogger<ProcessCollectionBatchesService> logger, IFlexHost flexHost, IFlexPrimaryKeyGeneratorBridge pkGenerator, ICustomUtility customUtility)
        {
            _bus = bus;
            _logger = logger;
            _flexHost = flexHost;
            _pkGenerator = pkGenerator;
            _customUtility = customUtility;
        }
    }
}