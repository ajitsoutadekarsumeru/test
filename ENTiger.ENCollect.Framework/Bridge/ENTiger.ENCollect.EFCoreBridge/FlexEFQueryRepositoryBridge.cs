using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public abstract class FlexEFQueryRepositoryBridge : FlexEFQueryRepository, IFlexQueryRepositoryBridge 
    {
        public FlexEFQueryRepositoryBridge(ILogger<FlexEFQueryRepositoryBridge> logger) : base(logger)
        {
        }
    }
}
