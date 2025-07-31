using Microsoft.Extensions.DependencyInjection;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class Dissolved : CollectionBatchWorkflowState
    {
        private IFlexHost _flexHost;

        public Dissolved()
        {
            _flexHost = FlexContainer.ServiceProvider.GetRequiredService<IFlexHost>();
            Name = this.GetType().Name;
            this.SetAdded();
        }

        #region State

        public override CollectionBatchWorkflowState CreateCollectionBatch() => this;

        public override CollectionBatchWorkflowState AddCollectionBatchInPayInSlip() => this;

        public override CollectionBatchWorkflowState Dissolve() => this;

        public override CollectionBatchWorkflowState AcknowledgeCollectionBatch() => this;

        public override CollectionBatchWorkflowState CreateCollectionBatchForPartner() => this;

        public override CollectionBatchWorkflowState AcknowledgeCollectionBatchForPartner() => this;

        #endregion State
    }
}