using Microsoft.Extensions.DependencyInjection;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class CollectionCollectedByCollector : ReceiptWorkflowState
    {
        private IFlexHost _flexHost;

        public CollectionCollectedByCollector()
        {
            _flexHost = FlexContainer.ServiceProvider.GetRequiredService<IFlexHost>();
            Name = this.GetType().Name;
            this.SetAdded();
        }

        public override ReceiptWorkflowState AllocateToCollector() => this;

        public override ReceiptWorkflowState CollectCollection() => this;
    }
}