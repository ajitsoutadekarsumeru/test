using Microsoft.Extensions.DependencyInjection;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class ReceiptAllocatedToCollector : ReceiptWorkflowState
    {
        private IFlexHost _flexHost;

        public ReceiptAllocatedToCollector()
        {
            _flexHost = FlexContainer.ServiceProvider.GetRequiredService<IFlexHost>();
            Name = this.GetType().Name;
            this.SetAdded();
        }

        public override ReceiptWorkflowState AllocateToCollector() => this;

        public override ReceiptWorkflowState CollectCollection()
        {
            return _flexHost.GetFlexStateInstance<CollectionCollectedByCollector>();
        }
    }
}