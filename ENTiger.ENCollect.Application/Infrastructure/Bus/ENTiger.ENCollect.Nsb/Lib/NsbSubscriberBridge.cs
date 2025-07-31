using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class NsbSubscriberBridge<T> : NsbSubscriber<T> where T : FlexEvent
    {
        public override async Task Handle(T message, IMessageHandlerContext context)
        {
            await base.Handle(message, context);
        }
    }
}