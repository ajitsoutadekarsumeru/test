using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class NsbCommandHandlerBridge<T> : NsbCommandHandler<T> where T : FlexCommand
    {
        public override async Task Handle(T message, IMessageHandlerContext context)
        {
            await base.Handle(message, context);
        }
    }
}