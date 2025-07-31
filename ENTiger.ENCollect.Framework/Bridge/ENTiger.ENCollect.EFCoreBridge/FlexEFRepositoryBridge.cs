using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public abstract class FlexEFRepositoryBridge : FlexEFRepository, IFlexRepositoryBridge 
    {
        public FlexEFRepositoryBridge(ILogger<FlexEFRepositoryBridge> logger) : base(logger)
        {
        }
    }
}
