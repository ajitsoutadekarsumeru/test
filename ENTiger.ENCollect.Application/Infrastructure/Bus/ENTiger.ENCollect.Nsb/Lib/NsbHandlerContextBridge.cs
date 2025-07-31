using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class NsbHandlerContextBridge : NSbHandlerContext, IFlexServiceBusContextBridge
    {
        public NsbHandlerContextBridge(IMessageHandlerContext messageHandlerContext) : base(messageHandlerContext)
        {
        }
    }
}