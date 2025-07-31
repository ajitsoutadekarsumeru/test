using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class FlexServiceBusContextBridge : FlexServiceBusContext, IFlexServiceBusContextBridge
    {
        public FlexServiceBusContextBridge(IFlexServiceBus flexServiceBus) : base(flexServiceBus)
        {
        }
    }
}